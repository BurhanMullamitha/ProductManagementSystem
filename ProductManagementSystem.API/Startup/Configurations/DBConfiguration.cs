using ProductManagementSystem.Infrastructure;

namespace ProductManagementSystem.API.Startup.Configurations
{
    public static class DBConfiguration
    {
        public static void AddDbContext(this WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetSection("Database:ConnectionString").Value!;
            string databaseName = builder.Configuration.GetSection("Database:Name").Value!;

            builder.Services.AddSingleton(new MongoDBContext(connectionString, databaseName));
        }
    }
}
