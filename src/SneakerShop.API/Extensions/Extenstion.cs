using Microsoft.EntityFrameworkCore;
using SneakerShop.Application.Common.Services;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data;
using SneakerShop.Infrastructure.Data.Repositories;

namespace SneakerShop.API.Extensions
{
    public static class Extenstion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            // Application services
            services.AddScoped<ProductService>();

            return services;
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}