using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> GetAchievements()
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
            return new
            {
                AchievementId = badge.Id,
                badge.Name,
                badge.Description,
                badge.Icon,
                badge.UnlockCondition,
                badge.Threshold,
                IsUnlocked = userBadge is not null,
                userBadge?.UnlockedAt
            };
        }));
    }

    [HttpPost("evaluate")]
    public async Task<IActionResult> EvaluateAchievements()
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

        return Ok(new
        {
            NewlyUnlocked = newlyUnlocked.Select(badge => new
            {
                AchievementId = badge.Id,
                badge.Name,
                badge.Description,
                badge.Icon
            })
        });
    }

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }
}
