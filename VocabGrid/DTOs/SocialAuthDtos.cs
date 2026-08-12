using System.ComponentModel.DataAnnotations;

namespace VocabGrid.DTOs
{
    public class GoogleAuthDto
    {
        public string IdToken { get; set; } = string.Empty;
    }

    public class AppleAuthDto
    {
        public string IdToken { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; } // <--- Must be string? so .Trim() works!
    }
}