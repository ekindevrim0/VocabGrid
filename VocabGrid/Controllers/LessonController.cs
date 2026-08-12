using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.DTOs;
using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LessonController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public LessonController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetLessons([FromQuery] string? level = null)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        var lessons = await _unitOfWork.Repository<Lesson>().GetAllAsync();
        var query = lessons.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(level))
        {
            query = query.Where(lesson =>
                lesson.Level.Equals(level.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        return Ok(query
            .OrderBy(lesson => lesson.OrderIndex)
            .Select(lesson => new
            {
                LessonId = lesson.LessonID,
                lesson.Title,
                lesson.Description,
                lesson.Level,
                lesson.OrderIndex,
                lesson.CreatedAt
            }));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetLesson(int id)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(id);
        if (lesson is null)
        {
            return NotFound("Lesson not found.");
        }

        var links = await _unitOfWork.Repository<LessonVocabulary>()
            .FindAsync(link => link.LessonID == id);
        var wordIds = links.Select(link => link.WordID).ToHashSet();
        var words = wordIds.Count == 0
            ? new List<Vocabulary>()
            : (await _unitOfWork.Repository<Vocabulary>()
                    .FindAsync(word => wordIds.Contains(word.WordID)))
                .OrderBy(word => word.WordID)
                .ToList();

        return Ok(new
        {
            LessonId = lesson.LessonID,
            lesson.Title,
            lesson.Description,
            lesson.Level,
            lesson.OrderIndex,
            lesson.CreatedAt,
            Words = words.Select(word => new
            {
                WordId = word.WordID,
                word.Term,
                word.Translation,
                word.ExampleSentence,
                word.ImageUrl,
                word.AudioUrl
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson([FromBody] CreateLessonDto dto)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var lesson = new Lesson
        {
            Title = dto.Title.Trim(),
            Description = dto.Description?.Trim() ?? string.Empty,
            Level = string.IsNullOrWhiteSpace(dto.Level) ? "A1" : dto.Level.Trim(),
            OrderIndex = dto.OrderIndex,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<Lesson>().AddAsync(lesson);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetLesson), new { id = lesson.LessonID }, new
        {
            LessonId = lesson.LessonID,
            lesson.Title,
            lesson.Description,
            lesson.Level,
            lesson.OrderIndex,
            lesson.CreatedAt
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLesson(int id, [FromBody] UpdateLessonDto dto)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(id);
        if (lesson is null)
        {
            return NotFound("Lesson not found.");
        }

        lesson.Title = dto.Title.Trim();
        lesson.Description = dto.Description?.Trim() ?? string.Empty;
        lesson.Level = string.IsNullOrWhiteSpace(dto.Level) ? "A1" : dto.Level.Trim();
        lesson.OrderIndex = dto.OrderIndex;

        _unitOfWork.Repository<Lesson>().Update(lesson);
        await _unitOfWork.CompleteAsync();

        return Ok(new
        {
            LessonId = lesson.LessonID,
            lesson.Title,
            lesson.Description,
            lesson.Level,
            lesson.OrderIndex,
            lesson.CreatedAt
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(id);
        if (lesson is null)
        {
            return NotFound("Lesson not found.");
        }

        var links = await _unitOfWork.Repository<LessonVocabulary>()
            .FindAsync(link => link.LessonID == id);
        foreach (var link in links)
        {
            _unitOfWork.Repository<LessonVocabulary>().Delete(link);
        }

        _unitOfWork.Repository<Lesson>().Delete(lesson);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpPost("{id:int}/words")]
    public async Task<IActionResult> AttachWord(int id, [FromBody] AttachLessonWordDto dto)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(id);
        if (lesson is null)
        {
            return NotFound("Lesson not found.");
        }

        var word = new Vocabulary
        {
            DeckId = null,
            Term = dto.Term.Trim(),
            Translation = dto.Translation.Trim(),
            ExampleSentence = string.IsNullOrWhiteSpace(dto.ExampleSentence) ? null : dto.ExampleSentence.Trim(),
            ImageUrl = string.IsNullOrWhiteSpace(dto.ImageUrl) ? null : dto.ImageUrl.Trim(),
            AudioUrl = string.IsNullOrWhiteSpace(dto.AudioUrl) ? null : dto.AudioUrl.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<Vocabulary>().AddAsync(word);
        await _unitOfWork.CompleteAsync();

        await _unitOfWork.Repository<LessonVocabulary>().AddAsync(new LessonVocabulary
        {
            LessonID = id,
            WordID = word.WordID
        });
        await _unitOfWork.CompleteAsync();

        return Ok(new
        {
            LessonId = id,
            WordId = word.WordID,
            word.Term,
            word.Translation,
            word.ExampleSentence,
            word.ImageUrl,
            word.AudioUrl
        });
    }

    [HttpDelete("{id:int}/words/{wordId:int}")]
    public async Task<IActionResult> DetachWord(int id, int wordId)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        var link = (await _unitOfWork.Repository<LessonVocabulary>()
                .FindAsync(candidate => candidate.LessonID == id && candidate.WordID == wordId))
            .FirstOrDefault();

        if (link is null)
        {
            return NotFound("Lesson word not found.");
        }

        _unitOfWork.Repository<LessonVocabulary>().Delete(link);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }
}
