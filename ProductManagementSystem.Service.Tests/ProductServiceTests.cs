using Moq;
using Microsoft.Extensions.Logging;
using ProductManagementSystem.Dal.Abstractions;
using ProductManagementSystem.Dal.DTOs;
using ProductManagementSystem.Domain.Entities;

namespace ProductManagementSystem.Service.Tests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepoMock = new Mock<IProductRepository>();
    private readonly Mock<ICategoryRepository> _categoryRepoMock = new Mock<ICategoryRepository>();
    private readonly Mock<ILogger<ProductService>> _loggerMock = new Mock<ILogger<ProductService>>();

    [Fact]
    public async Task CreateProductAsync_ShouldReturnSuccess_WhenCategoryExists()
    {
        // Arrange
        var productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object, _loggerMock.Object);
        var product = new Product { CategoryId = "1" };
        _categoryRepoMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<string>())).ReturnsAsync(new Category());

        // Act
        var result = await productService.CreateProductAsync(product);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CreateProductAsync_ShouldReturnFailure_WhenCategoryDoesNotExist()
    {
        // Arrange
        var productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object, _loggerMock.Object);
        var product = new Product { CategoryId = "1" };
        _categoryRepoMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<string>())).ReturnsAsync((Category)null);

        // Act
        var result = await productService.CreateProductAsync(product);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Category not found", result.Error);
    }

    [Fact]
    public async Task DeleteProductAsync_ShouldReturnSuccess_WhenProductExists()
    {
        // Arrange
        var productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object, _loggerMock.Object);
        var id = "1";
        _productRepoMock.Setup(x => x.DeleteProductAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        // Act
        var result = await productService.DeleteProductAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnSuccess_WhenProductExists()
    {
        // Arrange
        var productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object, _loggerMock.Object);
        var id = "1";
        _productRepoMock.Setup(x => x.GetProductByIdAsync(It.IsAny<string>())).ReturnsAsync(new ProductDto());

        // Act
        var result = await productService.GetProductByIdAsync(id);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetProductsAsync_ShouldReturnSuccess_WhenProductsExist()
    {
        // Arrange
        var productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object, _loggerMock.Object);
        _productRepoMock.Setup(x => x.GetProductsAsync()).ReturnsAsync(new List<ProductDto>());

        // Act
        var result = await productService.GetProductsAsync();

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateProductAsync_ShouldReturnSuccess_WhenProductExists()
    {
        // Arrange
        var productService = new ProductService(_productRepoMock.Object, _categoryRepoMock.Object, _loggerMock.Object);
        var id = "1";
        var updatedProduct = new Product { CategoryId = "1" };
        _productRepoMock.Setup(x => x.GetProductByIdAsync(It.IsAny<string>())).ReturnsAsync(new ProductDto());
        _categoryRepoMock.Setup(x => x.GetCategoryByIdAsync(It.IsAny<string>())).ReturnsAsync(new Category());

        // Act
        var result = await productService.UpdateProductAsync(id, updatedProduct);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
