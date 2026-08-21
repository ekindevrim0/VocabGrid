namespace VocabGrid.Entities;

/// <summary>
/// E-posta doğrulama kodu kaydı. <see cref="PasswordResetToken"/> ile aynı
/// deseni izler; farkı, kullanıcıya 6 haneli kısa bir kod gösterildiği için
/// tahmin edilmeye çok daha açık olmasıdır. Bu yüzden ömrü kısadır ve
/// <see cref="AttemptCount"/> ile deneme sayısı sınırlanır.
/// </summary>
public class EmailVerificationToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    /// <summary>Kullanıcıya e-posta ile gönderilen 6 haneli kod.</summary>
    public string Code { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; }

    /// <summary>
    /// Bu kod için yapılan başarısız doğrulama denemesi sayısı. 6 haneli bir
    /// kodun arama uzayı yalnızca 1.000.000 olduğundan, sınırsız denemeye izin
    /// vermek kodu kaba kuvvetle bulunabilir hale getirir.
    /// </summary>
    public int AttemptCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
