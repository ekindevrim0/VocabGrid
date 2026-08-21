using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Controllers;

/// <summary>
/// Kelime etiketleri ve etikete göre kelime arama.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TagController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TagController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <param name="kind">Grammar, Register veya Difficulty ile daraltır.</param>
    [HttpGet]
    public async Task<IActionResult> GetTags([FromQuery] string? kind = null)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        var tags = await _unitOfWork.Repository<Tag>().GetAllAsync();
        var query = tags.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(kind))
        {
            query = query.Where(tag => tag.Kind.Equals(kind.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        return Ok(query
            .OrderBy(tag => tag.Kind)
            .ThenBy(tag => tag.Name)
            .Select(tag => new { tag.Id, tag.Name, tag.Slug, tag.Kind, tag.Description }));
    }

    /// <summary>
    /// Etiketteki kelimeler. Yalnızca müfredat kelimeleri ve isteyenin kendi
    /// kartları döner — başka bir kullanıcının destesindeki kart, etiketi
    /// paylaşsa bile görünmez.
    /// </summary>
    [HttpGet("{slug}/words")]
    public async Task<IActionResult> GetWordsByTag(string slug)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var tag = (await _unitOfWork.Repository<Tag>().FindAsync(t => t.Slug == slug)).FirstOrDefault();
        if (tag is null)
        {
            return NotFound(new { Message = $"'{slug}' diye bir etiket yok." });
        }

        var links = await _unitOfWork.Repository<VocabularyTag>().FindAsync(link => link.TagId == tag.Id);
        var wordIds = links.Select(link => link.WordID).ToHashSet();
        if (wordIds.Count == 0)
        {
            return Ok(Array.Empty<object>());
        }

        var words = await _unitOfWork.Repository<Vocabulary>()
            .FindAsync(word => wordIds.Contains(word.WordID));

        var ownDeckIds = (await _unitOfWork.Repository<Deck>().FindAsync(deck => deck.UserId == userId))
            .Select(deck => deck.Id)
            .ToHashSet();

        return Ok(words
            // DeckId null = müfredat kelimesi, herkese ait.
            .Where(word => word.DeckId is null || ownDeckIds.Contains(word.DeckId.Value))
            .OrderBy(word => word.Term)
            .Select(word => new
            {
                word.WordID,
                word.Term,
                word.Translation,
                word.ExampleSentence,
                word.DeckId
            }));
    }

    private int? TryGetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(claim, out var id) ? id : null;
    }
}
