using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagementSystem.API.Controllers;
using ProductManagementSystem.Dal.Core;
using ProductManagementSystem.Dal.DTOs;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Service.Abstractions;
using System.Net;

namespace ProductManagementSystem.API.Tests;

public class ProductsControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductsController(_mockProductService.Object);
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult_WhenProductsExist()
    {
        // Arrange
        var products = new List<ProductDto> { new ProductDto(), new ProductDto() };
        _mockProductService.Setup(service => service.GetProductsAsync())
            .ReturnsAsync(Result<List<ProductDto>>.Success(products));

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ProductDto>>(okResult.Value);
        Assert.Equal(products.Count, returnValue.Count);
    }

    [Fact]
    public async Task GetProducts_ReturnsOkResult_WhenNoProductsExist()
    {
        // Arrange
        _mockProductService.Setup(service => service.GetProductsAsync())
            .ReturnsAsync(Result<List<ProductDto>>.Success(new List<ProductDto>()));

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProducts_ReturnsObjectResult_WhenServiceFails()
    {
        // Arrange
        _mockProductService.Setup(service => service.GetProductsAsync())
            .ReturnsAsync(Result<List<ProductDto>>.Failure("Something went wrong while processing your request", (int)HttpStatusCode.InternalServerError));

        // Act
        var result = await _controller.Get();

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("Something went wrong while processing your request", problemDetails.Detail);
    }

    [Fact]
    public async Task GetProductById_ReturnsOkResult_WhenProductExists()
    {
        // Arrange
        var product = new ProductDto();
        _mockProductService.Setup(service => service.GetProductByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(Result<ProductDto>.Success(product));

        // Act
        var result = await _controller.Get("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProductDto>(okResult.Value);
        Assert.Equal(product, returnValue);
    }

    [Fact]
    public async Task CreateProduct_ReturnsOkResult_WhenCreationIsSuccessful()
    {
        // Arrange
        var product = new Product();
        var productDto = new ProductDto();
        _mockProductService.Setup(service => service.CreateProductAsync(It.IsAny<Product>()))
            .ReturnsAsync(Result<ProductDto>.Success(productDto));

        // Act
        var result = await _controller.Post(product);

        // Assert
        var createResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProductDto>(createResult.Value);
        Assert.Equal(productDto, returnValue);
    }

    [Fact]
    public async Task CreateProduct_ReturnsBadRequestResult_WhenCategoryIsNotFound()
    {
        // Arrange
        var product = new Product();
        var productDto = new ProductDto();
        _mockProductService.Setup(service => service.CreateProductAsync(It.IsAny<Product>()))
            .ReturnsAsync(Result<ProductDto>.Failure("Category not found", (int)HttpStatusCode.BadRequest));

        // Act
        var result = await _controller.Post(product);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(badRequestResult.Value);
        Assert.Equal("Category not found", problemDetails.Detail);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsOkResult_WhenUpdateIsSuccessful()
    {
        // Arrange
        var product = new Product();
        var productDto = new ProductDto();
        _mockProductService.Setup(service => service.UpdateProductAsync(It.IsAny<string>(), It.IsAny<Product>()))
            .ReturnsAsync(Result<ProductDto>.Success(productDto));

        // Act
        var result = await _controller.Put(product, "1");

        // Assert
        var updateResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProductDto>(updateResult.Value);
        Assert.Equal(productDto, returnValue);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsBadRequestResult_WhenCategoryIsNotFound()
    {
        // Arrange
        var product = new Product();
        var productDto = new ProductDto();
        _mockProductService.Setup(service => service.UpdateProductAsync(It.IsAny<string>(), It.IsAny<Product>()))
            .ReturnsAsync(Result<ProductDto>.Failure("Category not found", (int)HttpStatusCode.BadRequest));

        // Act
        var result = await _controller.Put(product, "1");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(badRequestResult.Value);
        Assert.Equal("Category not found", problemDetails.Detail);
    }
}