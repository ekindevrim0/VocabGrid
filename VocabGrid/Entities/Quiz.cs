using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// Kalıcı quiz soru bankası (ders bazlı).
/// Oturum skoru QuizSession'da tutulur; ephemeral sorular zorunlu saklanmaz.
/// </summary>
public class Quiz
{
    [Key]
    public int QuizID { get; set; }
    public int LessonID { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public string QuestionText { get; set; } = string.Empty;
    public string QuestionType { get; set; } = "MultipleChoice";

    /// <summary>Opsiyonel soru görseli.</summary>
    public string? ImageUrl { get; set; }

    public int Points { get; set; } = 1;
    public int TimeLimitSeconds { get; set; } = 20;

    public ICollection<QuizOption> Options { get; set; } = new List<QuizOption>();
}

public class QuizOption
{
    [Key]
    public int OptionID { get; set; }
    public int QuizID { get; set; }
    public Quiz Quiz { get; set; } = null!;

    public string OptionText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
