using Microsoft.Extensions.Logging;
using ProductManagementSystem.Dal.Abstractions;
using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Service.Abstractions;

namespace ProductManagementSystem.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly ILogger<ProductService> _logger;
    public ProductService(IProductRepository productRepo, ICategoryRepository categoryRepo, ILogger<ProductService> logger)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _logger = logger;
    }

    public async Task<Result<bool>> CreateProductAsync(Product product)
    {
        try
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
            {
                return Result<bool>.Failure("Category not found");
            }

            await _productRepo.CreateProductAsync(product);
            return Result<bool>.Success(true);
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<bool>.Failure("Failed to create product");
        }
    }

    public async Task<Result<bool>> DeleteProductAsync(string id)
    {
        try
        {
            await _productRepo.DeleteProductAsync(id);
            return Result<bool>.Success(true);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<bool>.Failure("Failed to delete product");
        }
    }

    public async Task<Result<Product>> GetProductByIdAsync(string id)
    {
        try
        {
            Product product = await _productRepo.GetProductByIdAsync(id);
            return Result<Product>.Success(product);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<Product>.Failure("Failed to fetch product");
        }
    }

    public async Task<Result<List<Product>>> GetProductsAsync()
    {
        try
        {
            List<Product> products = await _productRepo.GetProductsAsync();
            return Result<List<Product>>.Success(products);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<List<Product>>.Failure("Failed to fetch products");
        }
    }

    public async Task<Result<bool>> UpdateProductAsync(string id, Product updatedProduct)
    {
        try
        {
            var existingProduct = await _productRepo.GetProductByIdAsync(id);
            if(existingProduct == null)
            {
                return Result<bool>.Failure("Product does not exist");
            }

            var category = await _categoryRepo.GetCategoryByIdAsync(updatedProduct.CategoryId);
            if (category == null)
            {
                return Result<bool>.Failure("Category not found");
            }

            await _productRepo.UpdateProductAsync(id, updatedProduct);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<bool>.Failure("Failed to update product");
        }
    }
}
