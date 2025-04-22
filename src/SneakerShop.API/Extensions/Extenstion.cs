using Microsoft.EntityFrameworkCore;
using SneakerShop.Application.Common.Services;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data;
using SneakerShop.Infrastructure.Repositories;

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
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            // Application services
            services.AddScoped<ProductService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ProductImageService>();
            services.AddScoped<AuthService>();
            services.AddScoped<ReviewService>();

            return services;
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {

            services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigins",
                        builder =>
                        {
                            builder.WithOrigins("https://localhost:7183", "http://localhost:5142")
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}