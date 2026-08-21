using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.DTOs;
using VocabGrid.Entities;
using VocabGrid.Interfaces;
using VocabGrid.Services;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserProfileDto>> GetProfile()
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId.Value);
        if (user is null)
        {
            return NotFound("User not found.");
        }

        return Ok(MapProfile(user));
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
        {
            return BadRequest("FirstName and LastName are required.");
        }

        var userRepository = _unitOfWork.Repository<User>();
        var user = await userRepository.GetByIdAsync(userId.Value);
        if (user is null)
        {
            return NotFound("User not found.");
        }

        var previousTargetCode = user.TargetLanguageCode;

        user.FirstName = dto.FirstName.Trim();
        user.LastName = dto.LastName.Trim();
        user.AvatarUrl = string.IsNullOrWhiteSpace(dto.AvatarUrl) ? user.AvatarUrl : dto.AvatarUrl.Trim();
        user.NativeLanguage = string.IsNullOrWhiteSpace(dto.NativeLanguage) ? user.NativeLanguage : dto.NativeLanguage.Trim();
        user.TargetLanguage = string.IsNullOrWhiteSpace(dto.TargetLanguage) ? user.TargetLanguage : dto.TargetLanguage.Trim();
        user.NativeLanguageCode = string.IsNullOrWhiteSpace(dto.NativeLanguageCode)
            ? user.NativeLanguageCode
            : dto.NativeLanguageCode.Trim().ToLowerInvariant();
        user.TargetLanguageCode = string.IsNullOrWhiteSpace(dto.TargetLanguageCode)
            ? user.TargetLanguageCode
            : dto.TargetLanguageCode.Trim().ToLowerInvariant();
        user.TargetProficiencyLevel = string.IsNullOrWhiteSpace(dto.TargetProficiencyLevel)
            ? user.TargetProficiencyLevel
            : dto.TargetProficiencyLevel.Trim();
        user.DailyGoalMinutes = dto.DailyGoalMinutes > 0 ? dto.DailyGoalMinutes : user.DailyGoalMinutes;

        userRepository.Update(user);
        await _unitOfWork.CompleteAsync();

        // Hedef dil değiştiyse kategori desteleri de o dile geçmeli: eskiler
        // artık istenmeyen anahtarı taşır ve dokunulmamışlarsa yerlerini yeni
        // dildeki karşılıklarına bırakır. Dil aynı kaldıysa hiç uğraşmıyoruz —
        // senkronizasyon her çağrıda tüm şablonları okur.
        if (!string.Equals(previousTargetCode, user.TargetLanguageCode, StringComparison.OrdinalIgnoreCase))
        {
            await CategoryDeckSynchronizer.SyncAsync(_unitOfWork, userId.Value);
        }

        return Ok(new { Message = "Profile updated successfully.", Profile = MapProfile(user) });
    }

    [HttpGet("settings")]
    public async Task<IActionResult> GetSettings()
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var settings = await GetOrCreateSettingsAsync(userId.Value);
        return Ok(MapSettings(settings));
    }

    [HttpPut("settings")]
    public async Task<IActionResult> UpdateSettings([FromBody] UserSettingsDto dto)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var settingsRepository = _unitOfWork.Repository<UserSettings>();
        var settings = await GetOrCreateSettingsAsync(userId.Value);

        var previousDifficulty = settings.DifficultyMode;

        settings.DarkMode = dto.DarkMode;
        settings.DailyReminders = dto.DailyReminders;
        settings.SoundEffects = dto.SoundEffects;
        settings.ThemeColor = string.IsNullOrWhiteSpace(dto.ThemeColor) ? settings.ThemeColor : dto.ThemeColor.Trim();
        settings.TextSize = string.IsNullOrWhiteSpace(dto.TextSize) ? settings.TextSize : dto.TextSize.Trim();
        settings.DifficultyMode = string.IsNullOrWhiteSpace(dto.DifficultyMode)
            ? settings.DifficultyMode
            : dto.DifficultyMode.Trim();

        settingsRepository.Update(settings);
        await _unitOfWork.CompleteAsync();

        // Zorluk seviyesi kelime seçimini belirliyor: seviye yükseldiyse
        // kategori destelerine artık kapsama giren kartlar eklenir. Seviye
        // düştüğünde hiçbir şey silinmez — o kartlarda ilerleme olabilir.
        if (!string.Equals(previousDifficulty, settings.DifficultyMode, StringComparison.OrdinalIgnoreCase))
        {
            await CategoryDeckSynchronizer.SyncAsync(_unitOfWork, userId.Value);
        }

        return Ok(new { Message = "Settings updated successfully.", Settings = MapSettings(settings) });
    }

    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var userRepository = _unitOfWork.Repository<User>();
        var user = await userRepository.GetByIdAsync(userId.Value);
        if (user is null)
        {
            return Unauthorized();
        }

        if (!PasswordHasher.VerifyHash(dto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Current password is incorrect.");
        }

        PasswordHasher.CreateHash(dto.NewPassword, out var passwordHash, out var passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        // Changing the password invalidates any other device's session, the
        // same way a real reset does (AuthController.ResetPassword) --
        // otherwise a stolen refresh token would survive a password change.
        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;

        userRepository.Update(user);
        await _unitOfWork.CompleteAsync();

        return Ok(new { Message = "Password changed successfully." });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMyAccount()
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId.Value);
        if (user is null)
        {
            return Unauthorized();
        }

        // UserWordProgress, QuizSession, and StudyActivity are configured
        // NoAction (not Cascade) in AppDbContext -- SQL Server rejects
        // Users->X and Users->Decks->X both cascading at once for the same
        // table -- so they need deleting by hand before the User row
        // itself.
        var progressRepository = _unitOfWork.Repository<UserWordProgress>();
        foreach (var progress in await progressRepository.FindAsync(p => p.UserID == user.Id))
        {
            progressRepository.Delete(progress);
        }

        var sessionRepository = _unitOfWork.Repository<QuizSession>();
        foreach (var session in await sessionRepository.FindAsync(s => s.UserId == user.Id))
        {
            sessionRepository.Delete(session);
        }

        var activityRepository = _unitOfWork.Repository<StudyActivity>();
        foreach (var activity in await activityRepository.FindAsync(a => a.UserId == user.Id))
        {
            activityRepository.Delete(activity);
        }

        // Vocabulary->Deck is ClientCascade, not a real database cascade --
        // EF only deletes a deck's cards automatically when it has actually
        // loaded them into the change tracker as part of *this* operation.
        // Deleting the User (which does cascade to Decks at the database
        // level) never loads the Decks' Vocabulary rows, so without this
        // they'd still reference the about-to-be-deleted Deck and the whole
        // delete would fail with a FK violation. Deleting them explicitly
        // here, before the User row, sidesteps that.
        var deckRepository = _unitOfWork.Repository<Deck>();
        var deckIds = (await deckRepository.FindAsync(d => d.UserId == user.Id)).Select(d => d.Id).ToHashSet();
        var vocabularyRepository = _unitOfWork.Repository<Vocabulary>();
        foreach (var word in await vocabularyRepository.FindAsync(v => v.DeckId != null && deckIds.Contains(v.DeckId.Value)))
        {
            vocabularyRepository.Delete(word);
        }

        _unitOfWork.Repository<User>().Delete(user);
        await _unitOfWork.CompleteAsync();

        return Ok(new { Message = "Account deleted." });
    }

    [HttpGet("categories")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetMyCategories()
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var links = await _unitOfWork.Repository<UserCategory>()
            .FindAsync(link => link.UserId == userId.Value);
        var categoryIds = links.Select(link => link.CategoryId).ToHashSet();
        var categories = categoryIds.Count == 0
            ? new List<Category>()
            : (await _unitOfWork.Repository<Category>()
                    .FindAsync(category => categoryIds.Contains(category.Id)))
                .OrderBy(category => category.Id)
                .ToList();

        return Ok(categories.Select(MapCategoryDto));
    }

    [HttpPut("categories")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> ReplaceMyCategories([FromBody] ReplaceUserCategoriesDto dto)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var requestedIds = (dto.CategoryIds ?? new List<int>()).Distinct().ToList();
        if (requestedIds.Count > 0)
        {
            var existingCategories = await _unitOfWork.Repository<Category>()
                .FindAsync(category => requestedIds.Contains(category.Id));
            if (existingCategories.Count() != requestedIds.Count)
            {
                return BadRequest("One or more category ids are invalid.");
            }
        }

        var linkRepository = _unitOfWork.Repository<UserCategory>();
        var current = await linkRepository.FindAsync(link => link.UserId == userId.Value);
        foreach (var link in current)
        {
            linkRepository.Delete(link);
        }

        foreach (var categoryId in requestedIds)
        {
            await linkRepository.AddAsync(new UserCategory
            {
                UserId = userId.Value,
                CategoryId = categoryId
            });
        }

        await _unitOfWork.CompleteAsync();

        // Seçim kaydedildikten sonra kitaplığı ona göre kur: yeni kategorinin
        // destesi eklenir, çıkarılan kategorinin destesi dokunulmamışsa
        // kaldırılır. Kullanıcı bu ekranı kapattığında kitaplığın hazır olması
        // gerekiyor, bu yüzden arka plana atılmıyor.
        await CategoryDeckSynchronizer.SyncAsync(_unitOfWork, userId.Value);

        return await GetMyCategories();
    }

    [HttpGet("learning-purposes")]
    [ProducesResponseType(typeof(IEnumerable<LearningPurposeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LearningPurposeDto>>> GetMyLearningPurposes()
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var links = await _unitOfWork.Repository<UserLearningPurpose>()
            .FindAsync(link => link.UserId == userId.Value);
        var purposeIds = links.Select(link => link.LearningPurposeId).ToHashSet();
        var purposes = purposeIds.Count == 0
            ? new List<LearningPurpose>()
            : (await _unitOfWork.Repository<LearningPurpose>()
                    .FindAsync(purpose => purposeIds.Contains(purpose.Id)))
                .OrderBy(purpose => purpose.Id)
                .ToList();

        return Ok(purposes.Select(MapLearningPurposeDto));
    }

    [HttpPut("learning-purposes")]
    [ProducesResponseType(typeof(IEnumerable<LearningPurposeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LearningPurposeDto>>> ReplaceMyLearningPurposes([FromBody] ReplaceUserLearningPurposesDto dto)
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var requestedIds = dto.ResolvedIds();
        if (requestedIds.Count > 0)
        {
            var existingPurposes = await _unitOfWork.Repository<LearningPurpose>()
                .FindAsync(purpose => requestedIds.Contains(purpose.Id));
            if (existingPurposes.Count() != requestedIds.Count)
            {
                return BadRequest("One or more learning purpose ids are invalid.");
            }
        }

        var linkRepository = _unitOfWork.Repository<UserLearningPurpose>();
        var current = await linkRepository.FindAsync(link => link.UserId == userId.Value);
        foreach (var link in current)
        {
            linkRepository.Delete(link);
        }

        foreach (var purposeId in requestedIds)
        {
            await linkRepository.AddAsync(new UserLearningPurpose
            {
                UserId = userId.Value,
                LearningPurposeId = purposeId
            });
        }

        await _unitOfWork.CompleteAsync();
        return await GetMyLearningPurposes();
    }

    private async Task<UserSettings> GetOrCreateSettingsAsync(int userId)
    {
        var settingsRepository = _unitOfWork.Repository<UserSettings>();
        var existing = (await settingsRepository.FindAsync(s => s.UserId == userId)).FirstOrDefault();
        if (existing is not null)
        {
            return existing;
        }

        var created = new UserSettings { UserId = userId };
        await settingsRepository.AddAsync(created);
        await _unitOfWork.CompleteAsync();
        return created;
    }

    private static UserProfileDto MapProfile(User user) => new()
    {
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        AvatarUrl = user.AvatarUrl,
        NativeLanguage = user.NativeLanguage,
        TargetLanguage = user.TargetLanguage,
        NativeLanguageCode = user.NativeLanguageCode,
        TargetLanguageCode = user.TargetLanguageCode,
        TargetProficiencyLevel = user.TargetProficiencyLevel,
        DailyGoalMinutes = user.DailyGoalMinutes,
        CurrentStreak = user.CurrentStreak,
        LongestStreak = user.LongestStreak,
        Level = user.Level,
        TotalXp = user.TotalXp,
        IsPremium = user.IsPremium,
        IsEmailVerified = user.IsEmailVerified
    };

    private static CategoryDto MapCategoryDto(Category category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description,
        IconName = category.IconName,
        ColorHex = category.ColorHex
    };

    private static LearningPurposeDto MapLearningPurposeDto(LearningPurpose purpose) => new()
    {
        Id = purpose.Id,
        Name = purpose.Name,
        Description = purpose.Description
    };

    private static UserSettingsDto MapSettings(UserSettings settings) => new()
    {
        DarkMode = settings.DarkMode,
        DailyReminders = settings.DailyReminders,
        SoundEffects = settings.SoundEffects,
        ThemeColor = settings.ThemeColor,
        TextSize = settings.TextSize,
        DifficultyMode = settings.DifficultyMode
    };
}
