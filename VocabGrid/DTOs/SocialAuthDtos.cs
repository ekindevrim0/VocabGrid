using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public class GoogleAuthDto
{
    [Required]
    public string IdToken { get; set; } = string.Empty;
}

public class AppleAuthDto
{
    [Required]
    public string IdToken { get; set; } = string.Empty;

    /// <summary>Optional. Apple may only send the name on the first authorization.</summary>
    public string? Name { get; set; }
}
