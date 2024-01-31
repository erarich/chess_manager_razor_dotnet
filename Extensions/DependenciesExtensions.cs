using api_mvc.Data;
using Microsoft.EntityFrameworkCore;


namespace api_mvc.Extensions
{
    public static class DependenciesExtension
    {
        public static void SetupDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }


}
