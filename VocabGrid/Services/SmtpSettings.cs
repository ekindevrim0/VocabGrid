namespace VocabGrid.Services;

/// <summary>
/// SMTP ayarları. <see cref="Password"/> asla appsettings.json'a yazılmamalı —
/// User Secrets veya ortam değişkeni kullanın (bkz. <see cref="IsConfigured"/>).
/// </summary>
public class SmtpSettings
{
    public const string SectionName = "Smtp";

    public string Host { get; set; } = string.Empty;

    /// <summary>587 = STARTTLS (Gmail, Outlook), 465 = örtük SSL.</summary>
    public int Port { get; set; } = 587;

    public string User { get; set; } = string.Empty;

    /// <summary>
    /// Gmail için hesap şifreniz DEĞİL, iki adımlı doğrulama açıkken üretilen
    /// 16 haneli uygulama şifresi.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>Boşsa <see cref="User"/> kullanılır.</summary>
    public string FromAddress { get; set; } = string.Empty;

    public string FromName { get; set; } = "VocabGrid";

    public bool EnableSsl { get; set; } = true;

    /// <summary>
    /// Bu üçü olmadan hiçbir şey gönderilemez. Program.cs bu bayrağa bakarak
    /// gerçek gönderici ile log'a yazan stub arasında seçim yapar; böylece
    /// kimlik bilgisi olmayan bir geliştirici ortamı çalışmaya devam eder.
    /// </summary>
    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(Host) &&
        !string.IsNullOrWhiteSpace(User) &&
        !string.IsNullOrWhiteSpace(Password);
}
