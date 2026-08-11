namespace VocabGrid.DTOs;

public class GoogleAuthDto
{
    public string IdToken { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string GoogleId { get; set; } = string.Empty;
}

public class AppleAuthDto
{
    public string IdToken { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string AppleId { get; set; } = string.Empty;
}