namespace VocabGrid.Entities;

public class UserLearningPurpose
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int LearningPurposeId { get; set; }
    public LearningPurpose LearningPurpose { get; set; } = null!;
}
