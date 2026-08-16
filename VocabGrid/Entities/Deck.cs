using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// Kullanıcıya ait öğrenme destesi.
/// CardCount, DueCount, MasteryPercentage, ReviewsCount hesaplanır; saklanmaz.
/// </summary>
public class Deck
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    /// <summary>Opsiyonel kapak görseli.</summary>
    public string? CoverImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Vocabulary> Flashcards { get; set; } = new List<Vocabulary>();
}
