namespace VocabGrid.Entities;

/// <summary>
/// Ham çalışma aktivitesi. İstatistik, heatmap, accuracy ve study time buradan hesaplanır.
/// </summary>
public class StudyActivity
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    /// <summary>Review, Quiz, Lesson vb.</summary>
    public string ActivityType { get; set; } = "Review";

    public int? WordId { get; set; }
    public Vocabulary? Vocabulary { get; set; }

    public int? LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public int? DeckId { get; set; }
    public Deck? Deck { get; set; }

    /// <summary>Correct, Wrong, Skipped, Again, Hard, Medium, Easy</summary>
    public string? Result { get; set; }

    public int DurationSeconds { get; set; }
    public int XpEarned { get; set; }
}
