using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.Entities;
using VocabGrid.Interfaces;
using VocabGrid.Services;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StatisticsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public StatisticsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var period = BuildPeriod(from, to);
        if (period is null)
        {
            return BadRequest("from must be earlier than or equal to to.");
        }

        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId.Value);
        if (user is null)
        {
            return Unauthorized();
        }

        var activities = await GetActivitiesAsync(user.Id, period.Value.Start, period.Value.EndExclusive);
        var quizAnswers = activities
            .Where(activity => activity.ActivityType == "Quiz" && activity.Result is "Correct" or "Wrong")
            .ToList();
        var correctAnswers = quizAnswers.Count(activity => activity.Result == "Correct");
        var reviewCount = activities.Count(activity => activity.ActivityType == "Review");
        var completedLessons = (await _unitOfWork.Repository<UserProgress>()
                .FindAsync(progress => progress.UserID == user.Id && progress.Completed))
            .Count();
        var dueReviews = (await _unitOfWork.Repository<UserWordProgress>()
                .FindAsync(progress => progress.UserID == user.Id &&
                    (progress.NextReviewDate == null || progress.NextReviewDate <= DateTime.UtcNow)))
            .Count();
        // The selected period is for the overview metrics only. Streaks must use the
        // user's full activity history, otherwise a short date filter resets them.
        var activityHistory = await _unitOfWork.Repository<StudyActivity>()
            .FindAsync(activity => activity.UserId == user.Id);
        var activityDates = activityHistory.Select(activity => activity.OccurredAt);

        return Ok(new
        {
            Period = new { period.Value.Start, To = period.Value.EndExclusive.AddTicks(-1) },
            TotalStudySeconds = activities.Sum(activity => activity.DurationSeconds),
            TotalStudyMinutes = Math.Round(activities.Sum(activity => activity.DurationSeconds) / 60.0, 1),
            QuizAccuracyPercent = quizAnswers.Count == 0
                ? 0
                : Math.Round(correctAnswers * 100.0 / quizAnswers.Count, 1),
            QuizQuestionsAnswered = quizAnswers.Count,
            CorrectAnswers = correctAnswers,
            ReviewCount = reviewCount,
            CompletedLessons = completedLessons,
            DueReviews = dueReviews,
            CurrentStreak = StudyEngine.CalculateCurrentStreak(activityDates, DateTime.UtcNow),
            LongestStreak = Math.Max(user.LongestStreak, StudyEngine.CalculateLongestStreak(activityDates)),
            user.TotalXp,
            user.Level
        });
    }

    [HttpGet("heatmap")]
    public async Task<IActionResult> GetHeatmap([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var period = BuildPeriod(from, to);
        if (period is null)
        {
            return BadRequest("from must be earlier than or equal to to.");
        }

        var activities = await GetActivitiesAsync(userId.Value, period.Value.Start, period.Value.EndExclusive);
        var byDate = activities
            .GroupBy(activity => activity.OccurredAt.Date)
            .ToDictionary(
                group => group.Key,
                group => new
                {
                    StudySeconds = group.Sum(activity => activity.DurationSeconds),
                    Reviews = group.Count(activity => activity.ActivityType == "Review"),
                    QuizAnswers = group.Count(activity => activity.ActivityType == "Quiz" && activity.Result is "Correct" or "Wrong"),
                    XpEarned = group.Sum(activity => activity.XpEarned)
                });

        var days = new List<object>();
        for (var date = period.Value.Start.Date; date < period.Value.EndExclusive.Date; date = date.AddDays(1))
        {
            byDate.TryGetValue(date, out var summary);
            days.Add(new
            {
                Date = date,
                StudySeconds = summary?.StudySeconds ?? 0,
                Reviews = summary?.Reviews ?? 0,
                QuizAnswers = summary?.QuizAnswers ?? 0,
                XpEarned = summary?.XpEarned ?? 0
            });
        }

        return Ok(days);
    }

    private async Task<List<StudyActivity>> GetActivitiesAsync(int userId, DateTime start, DateTime endExclusive)
    {
        return (await _unitOfWork.Repository<StudyActivity>()
                .FindAsync(activity => activity.UserId == userId &&
                    activity.OccurredAt >= start && activity.OccurredAt < endExclusive))
            .OrderBy(activity => activity.OccurredAt)
            .ToList();
    }

    private static (DateTime Start, DateTime EndExclusive)? BuildPeriod(DateTime? from, DateTime? to)
    {
        var start = (from ?? DateTime.UtcNow.Date.AddDays(-29)).Date;
        var end = (to ?? DateTime.UtcNow).Date;
        if (start > end)
        {
            return null;
        }

        return (start, end.AddDays(1));
    }

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }
}
