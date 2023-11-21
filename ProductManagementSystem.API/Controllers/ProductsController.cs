using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Service.Abstractions;

namespace ProductManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseApiController
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return HandleResult(await _productService.GetProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return HandleResult(await _productService.GetProductByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product product)
    {
        return HandleResult(await _productService.CreateProductAsync(product));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Product product, string id)
    {
        return HandleResult(await _productService.UpdateProductAsync(id, product));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return HandleResult(await _productService.DeleteProductAsync(id));
    }
}
