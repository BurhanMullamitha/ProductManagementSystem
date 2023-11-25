using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Dal.DTOs;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.Service.Abstractions;

public interface IProductService
{
    Task<Result<List<ProductDto>>> GetProductsAsync();
    Task<Result<ProductDto>> GetProductByIdAsync(string id);
    Task<Result<ProductDto>> CreateProductAsync(Product product);
    Task<Result<ProductDto>> UpdateProductAsync(string id, Product product);
    Task<Result<bool>> DeleteProductAsync(string id);
}
