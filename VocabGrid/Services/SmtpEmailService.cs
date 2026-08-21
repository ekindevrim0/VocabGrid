using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using VocabGrid.Interfaces;

namespace VocabGrid.Services;

/// <summary>
/// Gerçek SMTP gönderimi. <see cref="EmailService"/> stub'ının aksine mesajı
/// gerçekten yollar; Program.cs yalnızca <see cref="SmtpSettings.IsConfigured"/>
/// doğruyken bunu kaydeder.
///
/// Yerleşik <see cref="SmtpClient"/> kullanılır — Gmail/Outlook'un STARTTLS
/// akışı için yeterli ve ek bağımlılık gerektirmez. OAuth2 ya da daha egzotik
/// sunucular gerekirse MailKit'e geçmek gerekir.
/// </summary>
public class SmtpEmailService : IEmailService
{
    private readonly SmtpSettings _settings;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(IOptions<SmtpSettings> settings, ILogger<SmtpEmailService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public Task SendPasswordResetEmailAsync(string email, string resetToken)
    {
        var body =
            $"""
             <p>Merhaba,</p>
             <p>VocabGrid hesabınız için şifre sıfırlama isteği aldık.
             Aşağıdaki kodu uygulamaya girin:</p>
             <p style="font-size:20px;font-weight:bold;letter-spacing:1px">{WebUtility.HtmlEncode(resetToken)}</p>
             <p>Bu isteği siz yapmadıysanız bu e-postayı yok sayabilirsiniz.</p>
             """;

        return SendAsync(email, "VocabGrid şifre sıfırlama", body);
    }

    public Task SendEmailVerificationCodeAsync(string email, string code)
    {
        var body =
            $"""
             <p>VocabGrid'e hoş geldiniz!</p>
             <p>Hesabınızı doğrulamak için bu kodu uygulamaya girin:</p>
             <p style="font-size:28px;font-weight:bold;letter-spacing:6px">{WebUtility.HtmlEncode(code)}</p>
             <p>Kod 15 dakika geçerlidir.</p>
             """;

        return SendAsync(email, "VocabGrid doğrulama kodunuz", body);
    }

    private async Task SendAsync(string to, string subject, string htmlBody)
    {
        var from = string.IsNullOrWhiteSpace(_settings.FromAddress) ? _settings.User : _settings.FromAddress;

        try
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = _settings.EnableSsl,
                Credentials = new NetworkCredential(_settings.User, _settings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            using var message = new MailMessage
            {
                From = new MailAddress(from, _settings.FromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            message.To.Add(to);

            await client.SendMailAsync(message);
            _logger.LogInformation("Email sent to {Email}: {Subject}", to, subject);
        }
        catch (Exception ex)
        {
            // Deliberately swallowed. Registration has already committed the user
            // and the code by the time this runs, so letting the exception escape
            // would return a 500 for an account that was in fact created. The
            // code stays valid in the database and "Resend code" gives the user a
            // second chance. Note the message never contains the code itself.
            _logger.LogError(ex, "Failed to send email to {Email}: {Subject}", to, subject);
        }
    }
}
