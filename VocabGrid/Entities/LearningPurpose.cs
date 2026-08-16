namespace VocabGrid.Entities;

/// <summary>
/// Öğrenme amacı tanımı (Travel, Business, Academic...). Kullanıcı çoklu seçer.
/// </summary>
public class LearningPurpose
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<UserLearningPurpose> UserLearningPurposes { get; set; } = new List<UserLearningPurpose>();
}
