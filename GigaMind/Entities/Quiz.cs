using System.ComponentModel.DataAnnotations;

namespace GigaMind.API.Entities;

public class Quiz
{
    [Key]
    public int QuizID { get; set; }
    public int LessonID { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public string QuestionText { get; set; } = string.Empty;
    public string QuestionType { get; set; } = "MultipleChoice";

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