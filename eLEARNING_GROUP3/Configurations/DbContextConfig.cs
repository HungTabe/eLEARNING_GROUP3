using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;

namespace OnlineLearning.Configurations
{
    public static class DbContextConfig
    {
        public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OnlLearnDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OLS")));

        }
    }
}
