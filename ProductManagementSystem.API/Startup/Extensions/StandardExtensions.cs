using ProductManagementSystem.API.Utilities.Middlewares;
using Serilog;

namespace ProductManagementSystem.API.Startup.Extensions;

public static class StandardExtensions
{
    public static void AddStandardServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));
    }

    public static void AddExceptionHandling(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
    }
}
