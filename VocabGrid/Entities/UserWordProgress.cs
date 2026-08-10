using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// Kullanıcıya özel flashcard / kelime SRS ilerlemesi.
/// Aynı Vocabulary kartı birden fazla kullanıcıda farklı state tutabilir.
/// </summary>
public class UserWordProgress
{
    [Key]
    public long UserWordID { get; set; }

    public int UserID { get; set; }
    public User User { get; set; } = null!;

    public int WordID { get; set; }
    public Vocabulary Vocabulary { get; set; } = null!;

    public int MasteryLevel { get; set; } = 0;
    public DateTime? NextReviewDate { get; set; }
    public DateTime LastReviewedAt { get; set; } = DateTime.UtcNow;

    public int ReviewCount { get; set; }
    public double EaseFactor { get; set; } = 2.5;
    public int IntervalDays { get; set; } = 0;

    /// <summary>Son değerlendirme: Again, Hard, Medium, Easy (Figma SRS).</summary>
    public string? LastRating { get; set; }
}
