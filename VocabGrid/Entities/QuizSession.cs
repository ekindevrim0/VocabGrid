namespace VocabGrid.Entities;

/// <summary>
/// Kullanıcının bir quiz oturumu (Figma: Q1 of 5, timer, pts).
/// Soru bankası Quiz tablosunda kalır; oturum skoru burada tutulur.
/// </summary>
public class QuizSession
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int? LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public int? DeckId { get; set; }
    public Deck? Deck { get; set; }

    public int TotalQuestions { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount { get; set; }
    public int SkippedCount { get; set; }
    public int ScorePoints { get; set; }
    public int TimeLimitSeconds { get; set; } = 20;

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public ICollection<QuizSessionAnswer> Answers { get; set; } = new List<QuizSessionAnswer>();
}

public class QuizSessionAnswer
{
    public long Id { get; set; }
    public int QuizSessionId { get; set; }
    public QuizSession QuizSession { get; set; } = null!;

    public int? QuizId { get; set; }
    public Quiz? Quiz { get; set; }

    public int? SelectedOptionId { get; set; }
    public QuizOption? SelectedOption { get; set; }

    public bool? IsCorrect { get; set; }
    public bool IsSkipped { get; set; }
    public int TimeSpentSeconds { get; set; }
    public int PointsEarned { get; set; }
}
