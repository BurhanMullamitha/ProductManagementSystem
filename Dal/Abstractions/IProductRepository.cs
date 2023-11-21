using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.Dal.Abstractions;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(string id, Product product);
    Task DeleteProductAsync(string id);
}
