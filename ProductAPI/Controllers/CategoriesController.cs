using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ModelsDto;
using Services.Services;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<CategoryDto>>> GetAll()
    {
        try
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetByID(int id)
    {
        try
        {
            var category = await _categoryService.GetByID(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
}