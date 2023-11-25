using ProductManagementSystem.API.Startup.Configurations;
using ProductManagementSystem.API.Startup.Extensions;
using ProductManagementSystem.API.Utilities.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContext();

builder.AddStandardServices();
builder.AddSwaggerServices();

builder.AddRepositories();
builder.AddServices();

builder.AddLogging();
builder.AddExceptionHandling();
builder.AddFluentValidations();

var app = builder.Build();

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseSerilogRequestLogging();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();