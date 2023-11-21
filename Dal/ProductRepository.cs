using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProductManagementSystem.Dal.Abstractions;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Infrastructure;

namespace ProductManagementSystem.Dal
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(MongoDBContext context)
        {
            _products = context.GetCollection<Product>("products");
        }

        public async Task CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _products.DeleteOneAsync(product => product.Id == id);
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        public async Task UpdateProductAsync(string id, Product updatedProduct)
        {
            await _products.ReplaceOneAsync(product => product.Id == id, updatedProduct);
        }
    }
}