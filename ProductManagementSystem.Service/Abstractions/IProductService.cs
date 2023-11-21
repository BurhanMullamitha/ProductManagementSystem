using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.Service.Abstractions;

public interface IProductService
{
    Task<Result<List<Product>>> GetProductsAsync();
    Task<Result<Product>> GetProductByIdAsync(string id);
    Task<Result<bool>> CreateProductAsync(Product product);
    Task<Result<bool>> UpdateProductAsync(string id, Product product);
    Task<Result<bool>> DeleteProductAsync(string id);
}
