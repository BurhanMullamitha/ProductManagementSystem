using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProductManagementSystem.Dal.Abstractions;
using ProductManagementSystem.Dal.DTOs;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Infrastructure;

namespace ProductManagementSystem.Dal
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<Category> _categories;
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMapper _mapper;

        public ProductRepository(MongoDBContext context, IMapper mapper, ILogger<ProductRepository> logger)
        {
            _products = context.GetCollection<Product>("products");
            _categories = context.GetCollection<Category>("categories");
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _products.DeleteOneAsync(product => product.Id == id);
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var aggregate = _products.Aggregate()
                                        .Match(p => p.Id == id)
                                        .Lookup("categories", "CategoryId", "_id", "Category")
                                        .Unwind("Category");

            var product = await aggregate.SingleOrDefaultAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var aggregate = _products.Aggregate()
                                    .Lookup("categories", "CategoryId", "_id", "Category")
                                    .Unwind("Category");

            var products = await aggregate.ToListAsync();

            var productDtos = products.Select(p => _mapper.Map<ProductDto>(p)).ToList();

            return productDtos;

        }

        public async Task UpdateProductAsync(string id, Product updatedProduct)
        {
            await _products.ReplaceOneAsync(product => product.Id == id, updatedProduct);
        }
    }
}