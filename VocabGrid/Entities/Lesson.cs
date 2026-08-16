using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

public class Lesson
{
    [Key]
    public int LessonID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Level { get; set; } = "A1";
    public int OrderIndex { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<LessonVocabulary> LessonVocabularies { get; set; } = new List<LessonVocabulary>();
    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
}
