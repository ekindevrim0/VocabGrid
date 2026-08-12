using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MediaController : ControllerBase
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp", ".gif", ".mp3", ".wav", ".m4a", ".ogg"
    };

    private readonly IWebHostEnvironment _environment;

    public MediaController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost("upload")]
    [RequestSizeLimit(10_000_000)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (TryGetUserId() is null)
        {
            return Unauthorized();
        }

        if (file is null || file.Length == 0)
        {
            return BadRequest("File is required.");
        }

        var extension = Path.GetExtension(file.FileName);
        if (string.IsNullOrWhiteSpace(extension) || !AllowedExtensions.Contains(extension))
        {
            return BadRequest("Unsupported file type.");
        }

        var webRoot = _environment.WebRootPath;
        if (string.IsNullOrWhiteSpace(webRoot))
        {
            webRoot = Path.Combine(_environment.ContentRootPath, "wwwroot");
        }

        var uploadsDir = Path.Combine(webRoot, "uploads");
        Directory.CreateDirectory(uploadsDir);

        var fileName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        var physicalPath = Path.Combine(uploadsDir, fileName);

        await using (var stream = System.IO.File.Create(physicalPath))
        {
            await file.CopyToAsync(stream);
        }

        var relativeUrl = $"/uploads/{fileName}";
        return Ok(new
        {
            Url = relativeUrl,
            FileName = fileName,
            ContentType = file.ContentType,
            SizeBytes = file.Length
        });
    }

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }
}
