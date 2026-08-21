using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// Kelimelere takılabilen etiket — "düzensiz fiil", "resmî", "günlük konuşma",
/// "yanlış dost" gibi.
///
/// <see cref="Category"/> ile karıştırılmamalı: kategori konu başlığıdır
/// (Yemek, Seyahat) ve kullanıcının ilgi alanlarını seçmesi için vardır.
/// Etiket ise kelimenin dilbilgisel ya da kullanım özelliğidir; bir kelime
/// birden çok etiket taşıyabilir ve etiketler konu bağımsızdır.
/// </summary>
public class Tag
{
    public int Id { get; set; }

    [MaxLength(60)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// URL ve sorgu parametresi için sabit biçim ("irregular-verb"). Ad
    /// çevrilebilir ya da düzeltilebilir, slug ise kalıcıdır — istemcinin
    /// kaydettiği filtre bir yeniden adlandırmayla bozulmasın diye.
    /// </summary>
    [MaxLength(60)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>Grammar, Register veya Difficulty — etiketin hangi eksende olduğu.</summary>
    [MaxLength(20)]
    public string Kind { get; set; } = "Grammar";

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    public ICollection<VocabularyTag> VocabularyTags { get; set; } = new List<VocabularyTag>();
}
