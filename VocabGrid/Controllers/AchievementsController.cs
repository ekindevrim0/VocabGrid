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
public class AchievementsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AchievementsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AchievementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AchievementDto>>> GetAchievements()
    {
        var userId = TryGetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var badges = (await _unitOfWork.Repository<Badge>().GetAllAsync()).OrderBy(badge => badge.Id).ToList();
        var userBadges = (await _unitOfWork.Repository<UserBadge>()
                .FindAsync(userBadge => userBadge.UserId == userId.Value))
            .ToDictionary(userBadge => userBadge.BadgeId);

        return Ok(badges.Select(badge =>
        {
            userBadges.TryGetValue(badge.Id, out var userBadge);
            var isSupported = AchievementEvaluator.IsSupported(badge);
            return new AchievementDto
            {
                AchievementId = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                Icon = badge.Icon,
                UnlockCondition = badge.UnlockCondition,
                Threshold = badge.Threshold,
                IsSupported = isSupported,
                UnsupportedReason = isSupported ? null : AchievementEvaluator.GetUnsupportedReason(badge),
                IsUnlocked = userBadge is not null,
                UnlockedAt = userBadge?.UnlockedAt
            };
        }));
    }

    [HttpPost("evaluate")]
    [ProducesResponseType(typeof(EvaluateAchievementsResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<EvaluateAchievementsResponseDto>> EvaluateAchievements()
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

        var newlyUnlocked = await AchievementEvaluator.UnlockEligibleAsync(_unitOfWork, user);
        if (newlyUnlocked.Count > 0)
        {
            await _unitOfWork.CompleteAsync();
        }

        return Ok(new EvaluateAchievementsResponseDto
        {
            NewlyUnlocked = newlyUnlocked.Select(badge => new NewlyUnlockedAchievementDto
            {
                AchievementId = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                Icon = badge.Icon
            }).ToList()
        });
    }

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }
}
