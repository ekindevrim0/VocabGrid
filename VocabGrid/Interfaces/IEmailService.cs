namespace VocabGrid.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email, string resetToken);

    Task SendEmailVerificationCodeAsync(string email, string code);
}
