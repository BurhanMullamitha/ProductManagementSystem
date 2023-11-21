using ProductManagementSystem.Service;
using ProductManagementSystem.Service.Abstractions;

namespace ProductManagementSystem.API.Startup.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
    }
}