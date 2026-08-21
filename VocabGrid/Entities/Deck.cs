using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// Kullanıcıya ait öğrenme destesi.
/// CardCount, DueCount, MasteryPercentage, ReviewsCount hesaplanır; saklanmaz.
/// </summary>
public class Deck
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    /// <summary>Opsiyonel kapak görseli.</summary>
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// Uygulamanın hesap açılışında oluşturduğu başlangıç destelerini işaretler:
    /// "basics_DE", "food_TR" gibi, konu kısaltması ve hedef dilin kodu.
    /// Kullanıcının kendi oluşturduğu destelerde null.
    ///
    /// Buna ihtiyaç var çünkü hedef dil değiştiğinde başlangıç destelerinin de
    /// yeni dille değişmesi gerekiyor ve hangi destenin uygulamadan geldiğini
    /// başlıktan anlamak güvenilmez: "Numbers" ve "Food &amp; Drink" dil adı
    /// taşımıyor, üstelik kullanıcı desteyi yeniden adlandırabiliyor.
    ///
    /// İstemci bu alanı yazar; sunucu yalnızca saklar ve geri verir. Anahtarın
    /// biçimini sunucu doğrulamaz — hangi konuların gönderileceği uygulamanın
    /// içeriğine bağlı ve burada tekrar edilmesi ikisini birbirine bağlardı.
    /// </summary>
    [MaxLength(40)]
    public string? StarterKey { get; set; }

    /// <summary>
    /// ISO dil kodu ("de", "tr", "en"...): bu deste hangi hedef dil için.
    /// Oluşturulduğunda öğrenenin o anki TargetLanguageCode'undan otomatik
    /// damgalanır -- istemcinin ayrıca göndermesi gerekmez.
    ///
    /// Deste listesi buna göre süzülür: öğrenen hedef dilini değiştirdiğinde
    /// eski dildeki desteler silinmez, yalnızca görünümden çıkar (ilerlemesi
    /// dururken) ve o dile geri dönüldüğünde aynen geri gelir.
    /// </summary>
    [MaxLength(10)]
    public string? LanguageCode { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Vocabulary> Flashcards { get; set; } = new List<Vocabulary>();
}
