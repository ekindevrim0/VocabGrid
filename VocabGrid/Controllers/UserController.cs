using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VocabGrid.DTOs;
using VocabGrid.Entities;
using VocabGrid.Repositories;

namespace VocabGrid.Controllers
{
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

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim!);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);

            if (user == null)
                return NotFound("User not found.");

            var profileDto = new UserProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                NativeLanguage = user.NativeLanguage,
                TargetLanguage = user.TargetLanguage,
                DailyGoalMinutes = user.DailyGoalMinutes
            };

            return Ok(profileDto);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
        {
            var userId = GetUserId();
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);

            if (user == null)
                return NotFound("User not found.");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.NativeLanguage = dto.NativeLanguage ?? string.Empty;
            user.TargetLanguage = dto.TargetLanguage ?? string.Empty;
            user.DailyGoalMinutes = dto.DailyGoalMinutes;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.CompleteAsync();

            return Ok(new { message = "Profile updated successfully." });
        }

        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var userId = GetUserId();
            var settings = (await _unitOfWork.Repository<UserSettings>()
                .FindAsync(s => s.UserId == userId)).FirstOrDefault();

            if (settings == null)
                return NotFound("Settings not found.");

            var settingsDto = new UserSettingsDto
            {
                DarkMode = settings.DarkMode,
                DailyReminders = settings.DailyReminders,
                SoundEffects = settings.SoundEffects,
                TextSize = settings.TextSize,
                DifficultyMode = settings.DifficultyMode
            };

            return Ok(settingsDto);
        }

        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] UserSettingsDto dto)
        {
            var userId = GetUserId();
            var settings = (await _unitOfWork.Repository<UserSettings>()
                .FindAsync(s => s.UserId == userId)).FirstOrDefault();

            if (settings == null)
                return NotFound("Settings not found.");

            settings.DarkMode = dto.DarkMode;
            settings.DailyReminders = dto.DailyReminders;
            settings.SoundEffects = dto.SoundEffects;
            settings.TextSize = dto.TextSize;
            settings.DifficultyMode = dto.DifficultyMode;

            _unitOfWork.Repository<UserSettings>().Update(settings);
            await _unitOfWork.CompleteAsync();

            return Ok(new { message = "Settings updated successfully." });
        }
    }
}