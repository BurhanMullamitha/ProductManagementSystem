using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Dal.DTOs;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.Dal.Abstractions;

public interface IProductRepository
{
    Task<List<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(string id);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(string id, Product product);
    Task DeleteProductAsync(string id);
}
