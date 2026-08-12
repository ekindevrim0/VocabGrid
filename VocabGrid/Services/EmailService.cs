using VocabGrid.Interfaces;

namespace VocabGrid.Services;

/// <summary>
/// Development stub. Replace with a real SMTP/provider implementation for production.
/// Never logs the raw reset token.
/// </summary>
public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendPasswordResetEmailAsync(string email, string resetToken)
    {
        _logger.LogInformation("Password reset email queued for {Email}.", email);
        return Task.CompletedTask;
    }
}
