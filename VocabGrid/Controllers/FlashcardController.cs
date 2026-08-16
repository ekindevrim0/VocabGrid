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
public class FlashcardController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public FlashcardController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetByDeck([FromQuery] int deckId)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        if (!await IsDeckOwnedByUserAsync(deckId, userId.Value))
        {
            return NotFound("Deck not found.");
        }

        var cards = (await _unitOfWork.Repository<Vocabulary>()
                .FindAsync(card => card.DeckId == deckId))
            .OrderBy(card => card.WordID)
            .Select(MapCard);

        return Ok(cards);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetFlashcard(int id)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var card = await _unitOfWork.Repository<Vocabulary>().GetByIdAsync(id);
        if (card is null || card.DeckId is null)
        {
            return NotFound("Flashcard not found.");
        }

        if (!await IsDeckOwnedByUserAsync(card.DeckId.Value, userId.Value))
        {
            return NotFound("Flashcard not found.");
        }

        return Ok(MapCard(card));
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlashcard([FromBody] CreateFlashcardDto dto)
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

        if (!await IsDeckOwnedByUserAsync(dto.DeckId, userId.Value))
        {
            return NotFound("Deck not found.");
        }

        var card = new Vocabulary
        {
            DeckId = dto.DeckId,
            Term = dto.Term.Trim(),
            Translation = dto.Translation.Trim(),
            ExampleSentence = string.IsNullOrWhiteSpace(dto.ExampleSentence) ? null : dto.ExampleSentence.Trim(),
            ImageUrl = string.IsNullOrWhiteSpace(dto.ImageUrl) ? null : dto.ImageUrl.Trim(),
            AudioUrl = string.IsNullOrWhiteSpace(dto.AudioUrl) ? null : dto.AudioUrl.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<Vocabulary>().AddAsync(card);

        var deck = await _unitOfWork.Repository<Deck>().GetByIdAsync(dto.DeckId);
        if (deck is not null)
        {
            deck.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Repository<Deck>().Update(deck);
        }

        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetFlashcard), new { id = card.WordID }, MapCard(card));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateFlashcard(int id, [FromBody] UpdateFlashcardDto dto)
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

        var card = await _unitOfWork.Repository<Vocabulary>().GetByIdAsync(id);
        if (card is null || card.DeckId is null)
        {
            return NotFound("Flashcard not found.");
        }

        if (!await IsDeckOwnedByUserAsync(card.DeckId.Value, userId.Value))
        {
            return NotFound("Flashcard not found.");
        }

        card.Term = dto.Term.Trim();
        card.Translation = dto.Translation.Trim();
        card.ExampleSentence = string.IsNullOrWhiteSpace(dto.ExampleSentence) ? null : dto.ExampleSentence.Trim();
        card.ImageUrl = string.IsNullOrWhiteSpace(dto.ImageUrl) ? null : dto.ImageUrl.Trim();
        card.AudioUrl = string.IsNullOrWhiteSpace(dto.AudioUrl) ? null : dto.AudioUrl.Trim();
        card.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<Vocabulary>().Update(card);

        var deck = await _unitOfWork.Repository<Deck>().GetByIdAsync(card.DeckId.Value);
        if (deck is not null)
        {
            deck.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Repository<Deck>().Update(deck);
        }

        await _unitOfWork.CompleteAsync();
        return Ok(MapCard(card));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFlashcard(int id)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var card = await _unitOfWork.Repository<Vocabulary>().GetByIdAsync(id);
        if (card is null || card.DeckId is null)
        {
            return NotFound("Flashcard not found.");
        }

        if (!await IsDeckOwnedByUserAsync(card.DeckId.Value, userId.Value))
        {
            return NotFound("Flashcard not found.");
        }

        var deckId = card.DeckId.Value;
        _unitOfWork.Repository<Vocabulary>().Delete(card);

        var deck = await _unitOfWork.Repository<Deck>().GetByIdAsync(deckId);
        if (deck is not null)
        {
            deck.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Repository<Deck>().Update(deck);
        }

        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

    private async Task<bool> IsDeckOwnedByUserAsync(int deckId, int userId)
    {
        var deck = await _unitOfWork.Repository<Deck>().GetByIdAsync(deckId);
        return deck?.UserId == userId;
    }

    private static object MapCard(Vocabulary card) => new
    {
        WordId = card.WordID,
        card.DeckId,
        card.Term,
        card.Translation,
        card.ExampleSentence,
        card.ImageUrl,
        card.AudioUrl,
        card.CreatedAt,
        card.UpdatedAt
    };

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }
}
