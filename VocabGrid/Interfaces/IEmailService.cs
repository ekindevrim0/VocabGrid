namespace VocabGrid.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email, string token);
}