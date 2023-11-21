using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.Service.Abstractions;

public interface ICategoryService
{
    Task<Result<List<Category>>> GetCategoriesAsync();
    Task<Result<Category>> GetCategoryByIdAsync(string id);
    Task<Result<bool>> CreateCategoryAsync(Category category);
    Task<Result<bool>> UpdateCategoryAsync(string id, Category category);
    Task<Result<bool>> DeleteCategoryAsync(string id);
}
