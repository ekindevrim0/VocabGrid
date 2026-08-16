using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VocabGrid.DTOs;
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
    [ProducesResponseType(typeof(IEnumerable<LearningPurposeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LearningPurposeDto>>> GetAll()
    {
        var purposes = (await _unitOfWork.Repository<LearningPurpose>().GetAllAsync())
            .OrderBy(purpose => purpose.Id)
            .Select(purpose => new LearningPurposeDto
            {
                Id = purpose.Id,
                Name = purpose.Name,
                Description = purpose.Description
            });

        return Ok(purposes);
    }
}
