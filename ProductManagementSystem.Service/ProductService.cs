using Microsoft.Extensions.Logging;
using ProductManagementSystem.Dal.Abstractions;
using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Dal.DTOs;
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

    public async Task<Result<ProductDto>> CreateProductAsync(Product product)
    {
        try
        {
            Category category = await _categoryRepo.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
            {
                return Result<ProductDto>.Failure("Category not found");
            }

            await _productRepo.CreateProductAsync(product);

            var updatedProductInfo = await GetProductByIdAsync(product.Id);
            if (updatedProductInfo.IsSuccess)
            {
                return Result<ProductDto>.Success(updatedProductInfo.Value);
            }
            else
            {
                return Result<ProductDto>.Failure("Failed to get updated product");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<ProductDto>.Failure("Failed to create product");
        }
    }

    public async Task<Result<bool>> DeleteProductAsync(string id)
    {
        try
        {
            await _productRepo.DeleteProductAsync(id);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<bool>.Failure("Failed to delete product");
        }
    }

    public async Task<Result<ProductDto>> GetProductByIdAsync(string id)
    {
        try
        {
            ProductDto product = await _productRepo.GetProductByIdAsync(id);
            return Result<ProductDto>.Success(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<ProductDto>.Failure("Failed to fetch product");
        }
    }

    public async Task<Result<List<ProductDto>>> GetProductsAsync()
    {
        try
        {
            List<ProductDto> products = await _productRepo.GetProductsAsync();
            return Result<List<ProductDto>>.Success(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<List<ProductDto>>.Failure("Failed to fetch products");
        }
    }

    public async Task<Result<ProductDto>> UpdateProductAsync(string id, Product updatedProduct)
    {
        try
        {
            var existingProduct = await _productRepo.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return Result<ProductDto>.Failure("Product does not exist");
            }

            var category = await _categoryRepo.GetCategoryByIdAsync(updatedProduct.CategoryId);
            if (category == null)
            {
                return Result<ProductDto>.Failure("Category not found");
            }

            await _productRepo.UpdateProductAsync(id, updatedProduct);

            var updatedProductInfo = await GetProductByIdAsync(id);
            if (updatedProductInfo.IsSuccess)
            {
                return Result<ProductDto>.Success(updatedProductInfo.Value);
            }
            else
            {
                return Result<ProductDto>.Failure("Failed to get updated product");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while performing database operation: @message", ex.Message);
            return Result<ProductDto>.Failure("Failed to update product");
        }
    }
}
