using DoYourself.Core.DAL;
using Microsoft.EntityFrameworkCore;

namespace DoYourself.API.Extensions
{
    public static class RegisterDbContext
    {
        public static IServiceCollection AddDbContext (this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("PostgreDb");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }
    }
}
