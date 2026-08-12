using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.DTOs;
using VocabGrid.Entities;
using VocabGrid.Interfaces;
using VocabGrid.Services;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProgressController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProgressController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("lessons")]
    public async Task<IActionResult> GetLessonProgress()
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var lessons = await _unitOfWork.Repository<Lesson>().GetAllAsync();
        var progressByLesson = (await _unitOfWork.Repository<UserProgress>()
                .FindAsync(progress => progress.UserID == userId.Value))
            .ToDictionary(progress => progress.LessonID);

        return Ok(lessons.OrderBy(lesson => lesson.OrderIndex).Select(lesson =>
        {
            progressByLesson.TryGetValue(lesson.LessonID, out var progress);
            return new
            {
                LessonId = lesson.LessonID,
                lesson.Title,
                lesson.Level,
                lesson.OrderIndex,
                Score = progress?.Score ?? 0,
                Completed = progress?.Completed ?? false,
                LastAccess = progress?.LastAccess
            };
        }));
    }

    [HttpPut("lessons/{lessonId:int}")]
    public async Task<IActionResult> UpdateLessonProgress(int lessonId, [FromBody] UpdateLessonProgressDto dto)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(lessonId);
        if (lesson is null)
        {
            return NotFound("Lesson not found.");
        }

        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId.Value);
        if (user is null)
        {
            return Unauthorized();
        }

        var progressRepository = _unitOfWork.Repository<UserProgress>();
        var progress = (await progressRepository.FindAsync(candidate =>
                candidate.UserID == user.Id && candidate.LessonID == lessonId))
            .FirstOrDefault();
        var occurredAt = DateTime.UtcNow;

        if (progress is null)
        {
            progress = new UserProgress
            {
                UserID = user.Id,
                LessonID = lessonId,
                Score = dto.Score,
                Completed = dto.Completed,
                LastAccess = occurredAt
            };
            await progressRepository.AddAsync(progress);
        }
        else
        {
            progress.Score = Math.Max(progress.Score, dto.Score);
            progress.Completed |= dto.Completed;
            progress.LastAccess = occurredAt;
            progressRepository.Update(progress);
        }

        var activity = new StudyActivity
        {
            UserId = user.Id,
            LessonId = lessonId,
            OccurredAt = occurredAt,
            ActivityType = "Lesson",
            Result = dto.Completed ? "Completed" : null,
            DurationSeconds = dto.StudyDurationSeconds,
            XpEarned = dto.Completed ? 5 : 0
        };
        await _unitOfWork.Repository<StudyActivity>().AddAsync(activity);

        user.TotalXp += activity.XpEarned;
        await StudyEngine.UpdateStreakAsync(_unitOfWork, user, occurredAt);
        var newlyUnlocked = await AchievementEvaluator.UnlockEligibleAsync(_unitOfWork, user, activity);
        await _unitOfWork.CompleteAsync();

        return Ok(new
        {
            LessonId = lesson.LessonID,
            lesson.Title,
            progress.Score,
            progress.Completed,
            progress.LastAccess,
            NewlyUnlockedAchievements = newlyUnlocked.Select(badge => new { badge.Id, badge.Name, badge.Description, badge.Icon })
        });
    }

    [HttpGet("reviews/due")]
    public async Task<IActionResult> GetDueReviews([FromQuery] int? deckId, [FromQuery] int take = 50)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        if (take is < 1 or > 100)
        {
            return BadRequest("take must be between 1 and 100.");
        }

        if (deckId is not null && !await IsDeckOwnedByUserAsync(deckId.Value, userId.Value))
        {
            return NotFound("Deck not found.");
        }

        var deckIds = deckId is not null
            ? new[] { deckId.Value }
            : (await _unitOfWork.Repository<Deck>()
                    .FindAsync(deck => deck.UserId == userId.Value))
                .Select(deck => deck.Id)
                .ToArray();
        if (deckIds.Length == 0)
        {
            return Ok(Array.Empty<object>());
        }

        var words = await _unitOfWork.Repository<Vocabulary>()
            .FindAsync(word => word.DeckId != null && deckIds.Contains(word.DeckId.Value));
        var now = DateTime.UtcNow;
        var progressesByWord = (await _unitOfWork.Repository<UserWordProgress>()
                .FindAsync(progress => progress.UserID == userId.Value))
            .ToDictionary(progress => progress.WordID);

        return Ok(words
            .Where(word => !progressesByWord.TryGetValue(word.WordID, out var progress) ||
                progress.NextReviewDate is null || progress.NextReviewDate <= now)
            .OrderBy(word => progressesByWord.TryGetValue(word.WordID, out var progress)
                ? progress.NextReviewDate ?? DateTime.MinValue
                : DateTime.MinValue)
            .Take(take)
            .Select(word =>
            {
                progressesByWord.TryGetValue(word.WordID, out var progress);
                return new
                {
                    WordId = word.WordID,
                    word.DeckId,
                    word.Term,
                    word.Translation,
                    word.ExampleSentence,
                    word.ImageUrl,
                    word.AudioUrl,
                    MasteryLevel = progress?.MasteryLevel ?? 0,
                    ReviewCount = progress?.ReviewCount ?? 0,
                    IntervalDays = progress?.IntervalDays ?? 0,
                    EaseFactor = progress?.EaseFactor ?? 2.5,
                    LastRating = progress?.LastRating,
                    NextReviewDate = progress?.NextReviewDate
                };
            }));
    }

    [HttpPost("reviews/{wordId:int}")]
    public async Task<IActionResult> SubmitReview(int wordId, [FromBody] SubmitReviewDto dto)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var word = await _unitOfWork.Repository<Vocabulary>().GetByIdAsync(wordId);
        if (word is null)
        {
            return NotFound("Flashcard not found.");
        }

        if (word.DeckId is not null && !await IsDeckOwnedByUserAsync(word.DeckId.Value, userId.Value))
        {
            return NotFound("Flashcard not found.");
        }

        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId.Value);
        if (user is null)
        {
            return Unauthorized();
        }

        var progressRepository = _unitOfWork.Repository<UserWordProgress>();
        var progress = (await progressRepository.FindAsync(candidate =>
                candidate.UserID == user.Id && candidate.WordID == word.WordID))
            .FirstOrDefault();
        var reviewedAt = DateTime.UtcNow;

        var isNewProgress = progress is null;
        if (isNewProgress)
        {
            progress = new UserWordProgress
            {
                UserID = user.Id,
                WordID = word.WordID,
                LastReviewedAt = reviewedAt
            };
        }

        var userWordProgress = progress!;

        var schedule = StudyEngine.CalculateReviewSchedule(
            userWordProgress.IntervalDays,
            userWordProgress.EaseFactor,
            dto.Rating,
            reviewedAt);

        userWordProgress.IntervalDays = schedule.IntervalDays;
        userWordProgress.EaseFactor = schedule.EaseFactor;
        userWordProgress.NextReviewDate = schedule.NextReviewDate;
        userWordProgress.LastReviewedAt = reviewedAt;
        userWordProgress.LastRating = dto.Rating;
        userWordProgress.ReviewCount++;
        userWordProgress.MasteryLevel = Math.Clamp(userWordProgress.MasteryLevel + schedule.MasteryDelta, 0, 5);
        if (isNewProgress)
        {
            await progressRepository.AddAsync(userWordProgress);
        }
        else
        {
            progressRepository.Update(userWordProgress);
        }

        var xpEarned = dto.Rating switch
        {
            "Easy" => 2,
            "Good" => 1,
            "Hard" => 1,
            _ => 0
        };
        var activity = new StudyActivity
        {
            UserId = user.Id,
            WordId = word.WordID,
            DeckId = word.DeckId,
            OccurredAt = reviewedAt,
            ActivityType = "Review",
            Result = dto.Rating,
            DurationSeconds = dto.DurationSeconds,
            XpEarned = xpEarned
        };
        await _unitOfWork.Repository<StudyActivity>().AddAsync(activity);

        user.TotalXp += xpEarned;
        await StudyEngine.UpdateStreakAsync(_unitOfWork, user, reviewedAt);
        var newlyUnlocked = await AchievementEvaluator.UnlockEligibleAsync(_unitOfWork, user, activity);
        await _unitOfWork.CompleteAsync();

        return Ok(new
        {
            WordId = word.WordID,
            dto.Rating,
            userWordProgress.MasteryLevel,
            userWordProgress.ReviewCount,
            userWordProgress.IntervalDays,
            userWordProgress.EaseFactor,
            userWordProgress.NextReviewDate,
            NewlyUnlockedAchievements = newlyUnlocked.Select(badge => new { badge.Id, badge.Name, badge.Description, badge.Icon })
        });
    }

    [HttpGet("streak")]
    public async Task<IActionResult> GetStreak()
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId.Value);
        if (user is null)
        {
            return Unauthorized();
        }

        var activityDates = (await _unitOfWork.Repository<StudyActivity>()
                .FindAsync(activity => activity.UserId == user.Id))
            .Select(activity => activity.OccurredAt);

        return Ok(new
        {
            CurrentStreak = StudyEngine.CalculateCurrentStreak(activityDates, DateTime.UtcNow),
            LongestStreak = Math.Max(user.LongestStreak, StudyEngine.CalculateLongestStreak(activityDates)),
            user.DailyGoalMinutes
        });
    }

    private async Task<bool> IsDeckOwnedByUserAsync(int deckId, int userId)
    {
        var deck = await _unitOfWork.Repository<Deck>().GetByIdAsync(deckId);
        return deck?.UserId == userId;
    }

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }
}
