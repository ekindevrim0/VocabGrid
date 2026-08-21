using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// Bir şablonun tek bir dildeki adı ve açıklaması.
///
/// Öğrenenin kendi (native) diline göre seçilir, hedef dile göre değil:
/// Almanca çalışan bir İngiliz "Technology" görür, "Technik" değil -- deste
/// listesi bir gezinme yüzeyi, öğrenenin akıcı okuyabilmesi gerekiyor.
/// Öğrenilen dille asıl temas kartların kendisinde (Term/Translation).
/// Karşılığı olmayan dil için çağıran taraf İngilizceye düşer, böylece dil
/// listesine yeni bir dil eklemek adsız deste üretemez.
/// </summary>
public class DeckTemplateLabel
{
    public int DeckTemplateId { get; set; }
    public DeckTemplate DeckTemplate { get; set; } = null!;

    /// <summary><see cref="Language.Code"/> ile aynı ISO kodu (<c>en</c>, <c>ja</c>).</summary>
    [MaxLength(10)]
    public string LanguageCode { get; set; } = string.Empty;

    /// <summary><c>Deck.Title</c> ile aynı sınırda tutulur.</summary>
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
}
