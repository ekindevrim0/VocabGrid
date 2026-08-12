using VocabGrid.Interfaces;

namespace VocabGrid.Services;

public class EmailService : IEmailService
{
    public Task SendPasswordResetEmailAsync(string email, string token)
    {
        // Stubbed email service for now
        Console.WriteLine($"[STUB EMAIL] Sent password reset token '{token}' to {email}");
        return Task.CompletedTask;
    }
}