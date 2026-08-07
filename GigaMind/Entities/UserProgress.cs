using System.ComponentModel.DataAnnotations;

namespace GigaMind.API.Entities;

public class UserProgress
{
    [Key]
    public int ProgressID { get; set; }
    public int UserID { get; set; }
    public User User { get; set; } = null!;

    public int LessonID { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public int Score { get; set; }
    public bool Completed { get; set; }
    public DateTime LastAccess { get; set; } = DateTime.UtcNow;
}