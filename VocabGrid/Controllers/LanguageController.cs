using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Controllers;

/// <summary>
/// Desteklenen dillerin listesi.
///
/// <see cref="AllowAnonymous"/>: bu liste kayıt ve onboarding ekranlarında,
/// yani kullanıcının henüz bir oturumu yokken gerekiyor. İçinde kişisel bir
/// bilgi yok, sabit bir katalog.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class LanguageController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public LanguageController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <param name="includeInactive">
    /// Kapatılmış dilleri de döndürür. Yönetim ekranları için; normal
    /// istemcinin seçim listesinde kapalı bir dil görünmemeli.
    /// </param>
    [HttpGet]
    public async Task<IActionResult> GetLanguages([FromQuery] bool includeInactive = false)
    {
        var languages = await _unitOfWork.Repository<Language>().GetAllAsync();

        return Ok(languages
            .Where(language => includeInactive || language.IsActive)
            .OrderBy(language => language.SortOrder)
            .Select(language => new
            {
                language.Code,
                language.Name,
                language.NativeName,
                language.FlagCode,
                language.IsActive
            }));
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetLanguage(string code)
    {
        var languages = await _unitOfWork.Repository<Language>()
            .FindAsync(language => language.Code == code);

        var match = languages.FirstOrDefault();
        if (match is null)
        {
            return NotFound(new { Message = $"'{code}' diye bir dil yok." });
        }

        return Ok(new
        {
            match.Code,
            match.Name,
            match.NativeName,
            match.FlagCode,
            match.IsActive
        });
    }
}
