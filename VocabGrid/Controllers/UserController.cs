using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.DTOs;
using VocabGrid.Entities;
using VocabGrid.Interfaces;

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
    public async Task<IActionResult> GetProfile()
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

        user.FirstName = dto.FirstName.Trim();
        user.LastName = dto.LastName.Trim();
        user.AvatarUrl = string.IsNullOrWhiteSpace(dto.AvatarUrl) ? user.AvatarUrl : dto.AvatarUrl.Trim();
        user.NativeLanguage = string.IsNullOrWhiteSpace(dto.NativeLanguage) ? user.NativeLanguage : dto.NativeLanguage.Trim();
        user.TargetLanguage = string.IsNullOrWhiteSpace(dto.TargetLanguage) ? user.TargetLanguage : dto.TargetLanguage.Trim();
        user.TargetProficiencyLevel = string.IsNullOrWhiteSpace(dto.TargetProficiencyLevel)
            ? user.TargetProficiencyLevel
            : dto.TargetProficiencyLevel.Trim();
        user.DailyGoalMinutes = dto.DailyGoalMinutes > 0 ? dto.DailyGoalMinutes : user.DailyGoalMinutes;

        userRepository.Update(user);
        await _unitOfWork.CompleteAsync();

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

        return Ok(new { Message = "Settings updated successfully.", Settings = MapSettings(settings) });
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetMyCategories()
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

        return Ok(categories.Select(category => new
        {
            category.Id,
            category.Name,
            category.Description
        }));
    }

    [HttpPut("categories")]
    public async Task<IActionResult> ReplaceMyCategories([FromBody] ReplaceUserCategoriesDto dto)
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
        return await GetMyCategories();
    }

    [HttpGet("learning-purposes")]
    public async Task<IActionResult> GetMyLearningPurposes()
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

        return Ok(purposes.Select(purpose => new
        {
            purpose.Id,
            purpose.Name,
            purpose.Description
        }));
    }

    [HttpPut("learning-purposes")]
    public async Task<IActionResult> ReplaceMyLearningPurposes([FromBody] ReplaceUserLearningPurposesDto dto)
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

        var requestedIds = (dto.LearningPurposeIds ?? new List<int>()).Distinct().ToList();
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
        TargetProficiencyLevel = user.TargetProficiencyLevel,
        DailyGoalMinutes = user.DailyGoalMinutes,
        CurrentStreak = user.CurrentStreak,
        LongestStreak = user.LongestStreak,
        Level = user.Level,
        TotalXp = user.TotalXp,
        IsPremium = user.IsPremium
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
