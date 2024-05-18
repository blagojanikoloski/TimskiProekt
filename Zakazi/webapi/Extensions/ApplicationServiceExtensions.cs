using Microsoft.EntityFrameworkCore;
using webapi.Domain.Services;
using webapi.Repository;
using webapi.Repository.Implementation;
using webapi.Repository.Interface;
using webapi.Service.Implementation;
using webapi.Service.Interface;

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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IZakaziUserService, ZakaziUserService>();
            services.AddScoped<IPostService, PostService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // template to register service
            services.AddScoped<IRequestRepository, RequestRepository>();

            return services;
        }
    }
}
