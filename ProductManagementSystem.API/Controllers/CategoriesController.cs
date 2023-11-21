using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Service.Abstractions;

namespace ProductManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseApiController
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return HandleResult(await _categoryService.GetCategoriesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return HandleResult(await _categoryService.GetCategoryByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(Category category)
    {
        return HandleResult(await _categoryService.CreateCategoryAsync(category));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Category category, string id)
    {
        return HandleResult(await _categoryService.UpdateCategoryAsync(id, category));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return HandleResult(await _categoryService.DeleteCategoryAsync(id));
    }
}
