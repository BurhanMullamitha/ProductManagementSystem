using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.Dal.Abstractions;
using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Service.Abstractions;
using System.Net;

namespace ProductManagementSystem.Service;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepo, ILogger<CategoryService> logger)
    {
        _categoryRepo = categoryRepo;
        _logger = logger;
    }

    public async Task<Result<bool>> CreateCategoryAsync(Category category)
    {
        try
        {
            await _categoryRepo.CreateCategoryAsync(category);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<bool>.Failure("Failed to add category", (int)HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<bool>> DeleteCategoryAsync(string id)
    {
        try
        {
            await _categoryRepo.DeleteCategoryAsync(id);
            return Result<bool>.Success(true);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<bool>.Failure("Failed to delete category", (int)HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<Category>> GetCategoryByIdAsync(string id)
    {
        try 
        {
            Category category = await _categoryRepo.GetCategoryByIdAsync(id);
            return Result<Category>.Success(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<Category>.Failure("Failed to fetch category", (int)HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<Category>>> GetCategoriesAsync()
    {
        try
        {
            List<Category> categories = await _categoryRepo.GetCategoriesAsync();
            return Result<List<Category>>.Success(categories);
        }
        catch(Exception ex) 
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<List<Category>>.Failure("Failed to fetch categories", (int)HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<bool>> UpdateCategoryAsync(string id, Category updatedCategory)
    {
        try
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Result<bool>.Failure("Category not found", (int)HttpStatusCode.BadRequest);
            }

            await _categoryRepo.UpdateCategoryAsync(id, updatedCategory);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<bool>.Failure("Failed to update category", (int)HttpStatusCode.InternalServerError);
        }
    }
}
