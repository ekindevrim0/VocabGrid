using System.ComponentModel.DataAnnotations;

namespace VocabGrid.API.Entities;

public class Vocabulary
{
    [Key]
    public int WordID { get; set; }
    public string Term { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;
    public string? ExampleSentence { get; set; }
    public string? AudioUrl { get; set; }

    // Navigation Properties
    public ICollection<LessonVocabulary> LessonVocabularies { get; set; } = new List<LessonVocabulary>();
    public ICollection<UserWordProgress> UserWordProgresses { get; set; } = new List<UserWordProgress>();
}