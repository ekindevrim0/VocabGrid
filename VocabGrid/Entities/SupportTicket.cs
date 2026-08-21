using System.ComponentModel.DataAnnotations;

namespace VocabGrid.Entities;

/// <summary>
/// A problem report submitted from the app's Help &amp; Support screen.
/// No triage/response workflow yet -- this exists so "Report a Problem" is
/// a real submission instead of a "coming soon" snackbar; a support team
/// reads these directly against the database for now.
/// </summary>
public class SupportTicket
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [MaxLength(4000)]
    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
