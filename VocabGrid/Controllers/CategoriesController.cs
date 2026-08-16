using Microsoft.AspNetCore.Mvc;
using VocabGrid.DTOs;
using VocabGrid.Entities;
using VocabGrid.Interfaces;

namespace VocabGrid.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// List categories. Optional search: ?q=food
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories([FromQuery] string? q = null)
    {
        var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
        var query = categories.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(category =>
                category.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                (category.Description?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        return Ok(query
            .OrderBy(category => category.Id)
            .Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IconName = category.IconName,
                ColorHex = category.ColorHex
            }));
    }
}
