using Microsoft.EntityFrameworkCore;
using webapi.Repository;

namespace Time.Extensions
{
    public static class ApplicationsServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // template to register service
            //services.AddScoped<IService, Service>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // template to register service
            //services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
