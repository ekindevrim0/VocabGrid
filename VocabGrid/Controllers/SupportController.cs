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
public class SupportController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SupportController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("report")]
    public async Task<IActionResult> ReportProblem([FromBody] ReportProblemDto dto)
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

        var ticket = new SupportTicket
        {
            UserId = userId.Value,
            Message = dto.Message.Trim(),
            CreatedAt = DateTime.UtcNow,
        };
        await _unitOfWork.Repository<SupportTicket>().AddAsync(ticket);
        await _unitOfWork.CompleteAsync();

        return Ok(new { Message = "Thanks -- your report has been sent." });
    }

    private int? TryGetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(raw, out var id) ? id : null;
    }
}
