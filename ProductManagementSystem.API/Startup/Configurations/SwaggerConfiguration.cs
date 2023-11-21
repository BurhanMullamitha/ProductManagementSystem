namespace ProductManagementSystem.API.Startup.Configurations;

public static class SwaggerConfiguration
{
    public static WebApplication ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("v0/swagger.json", "v0");
        });

        return app;
    }
}