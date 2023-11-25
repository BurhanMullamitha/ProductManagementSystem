using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.Dal.Abstractions;

public interface ICategoryRepository
{
    Task<List<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(string id);
    Task CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(string id, Category category);
    Task DeleteCategoryAsync(string id);
}
