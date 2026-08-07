using System.ComponentModel.DataAnnotations;

namespace GigaMind.API.Entities;

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
}