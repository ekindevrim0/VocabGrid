namespace VocabGrid.Entities;

/// <summary>
/// Kelime ile etiket arasındaki çoka-çok bağı.
/// </summary>
public class VocabularyTag
{
    public int WordID { get; set; }
    public Vocabulary Vocabulary { get; set; } = null!;

    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}
