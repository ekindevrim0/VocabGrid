using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
