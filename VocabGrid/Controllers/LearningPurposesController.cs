using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LearningPurposesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public LearningPurposesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var purposes = (await _unitOfWork.Repository<LearningPurpose>().GetAllAsync())
            .OrderBy(purpose => purpose.Id)
            .Select(purpose => new
            {
                purpose.Id,
                purpose.Name,
                purpose.Description
            });

        return Ok(purposes);
    }
}
