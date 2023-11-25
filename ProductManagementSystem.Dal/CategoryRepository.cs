using MongoDB.Driver;
using ProductManagementSystem.Dal.Abstractions;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Infrastructure;

namespace ProductManagementSystem.Dal;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categories;

    public CategoryRepository(MongoDBContext context)
    {
        _categories = context.GetCollection<Category>("categories");
    }

    public async Task CreateCategoryAsync(Category category)
    {
        await _categories.InsertOneAsync(category);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _categories.DeleteOneAsync(category => category.Id == id);
    }

    public async Task<Category> GetCategoryByIdAsync(string id)
    {
        return await _categories.Find(category => category.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _categories.Find(category => true).ToListAsync();
    }

    public async Task UpdateCategoryAsync(string id, Category updatedProduct)
    {
        await _categories.ReplaceOneAsync(category => category.Id == id, updatedProduct);
    }
}
