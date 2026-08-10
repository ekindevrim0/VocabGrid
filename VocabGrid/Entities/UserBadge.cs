namespace VocabGrid.Entities;

/// <summary>
/// Kullanıcının açtığı rozet kaydı (Badge tanımı N:M User).
/// </summary>
public class UserBadge
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int BadgeId { get; set; }
    public Badge Badge { get; set; } = null!;

    public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
}
