using ProductManagementSystem.Dal;
using ProductManagementSystem.Dal.Abstractions;

namespace ProductManagementSystem.API.Startup.Extensions;

public static class RepositoryExtensions
{
    public static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
