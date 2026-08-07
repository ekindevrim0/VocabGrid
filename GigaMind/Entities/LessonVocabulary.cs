namespace GigaMind.API.Entities;

public class LessonVocabulary
{
    public int LessonID { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public int WordID { get; set; }
    public Vocabulary Vocabulary { get; set; } = null!;
}