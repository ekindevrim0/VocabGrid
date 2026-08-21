using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// Uygulamanın desteklediği diller.
///
/// Şimdiye kadar dil bilgisi iki yerde birden yaşıyordu: kullanıcı satırında
/// serbest metin olarak (<see cref="User.NativeLanguage"/>) ve istemcinin
/// içine gömülü sabit bir listede. İkisi birbirinden habersizdi — istemciye
/// yeni bir dil eklemek sunucuda karşılığı olmayan bir değer üretebiliyordu.
///
/// Bu tablo tek doğru kaynağı sunucuya taşıyor: istemci listeyi
/// <c>GET /api/Language</c> ile alır, dil eklemek için uygulamayı yeniden
/// yayınlamak gerekmez.
///
/// Anahtar olarak ISO kodu kullanılıyor, üretilen bir kimlik değil: kod zaten
/// benzersiz ve sabit, kullanıcı satırındaki <c>NativeLanguageCode</c> ile
/// doğrudan eşleşiyor.
/// </summary>
public class Language
{
    [Key]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    /// <summary>İngilizce adı — "Turkish".</summary>
    [MaxLength(60)]
    public string Name { get; set; } = string.Empty;

    /// <summary>Kendi dilindeki adı — "Türkçe". Dil seçicide bu gösterilir.</summary>
    [MaxLength(60)]
    public string NativeName { get; set; } = string.Empty;

    /// <summary>
    /// Bayrak için ülke kodu. Dilin kodundan farklı olabilir: İngilizce "en"
    /// ama bayrağı "gb", Japonca "ja" ama bayrağı "jp".
    /// </summary>
    [MaxLength(10)]
    public string FlagCode { get; set; } = string.Empty;

    /// <summary>Seçim listesindeki sıra; alfabetik değil, yaygınlığa göre.</summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// Kapatılan bir dil listeden düşer ama silinmez — o dili seçmiş
    /// kullanıcıların profili geçersiz bir koda işaret etmeye başlamasın diye.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
