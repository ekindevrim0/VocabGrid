using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = (await _unitOfWork.Repository<Category>().GetAllAsync())
            .OrderBy(category => category.Id)
            .Select(category => new
            {
                category.Id,
                category.Name,
                category.Description
            });

        return Ok(categories);
    }
}
