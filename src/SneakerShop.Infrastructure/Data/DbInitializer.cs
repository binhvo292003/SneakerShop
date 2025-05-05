using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SneakerShop.Domain.Entities;
using Microsoft.Extensions.Hosting;

namespace SneakerShop.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IHost host)
        {
            // define scope
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<StoreContext>();

                await context.Database.MigrateAsync();

                await SeedDataAsync(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                throw;
            }
        }

        private static async Task SeedDataAsync(StoreContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Man"},
                    new Category { Name = "Woman"},
                    new Category { Name = "High Top"},
                    new Category { Name = "Low Top"},
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        
                        Name = "Air Max 90",
                        Description = "Iconic Nike Air Max 90 sneakers with visible air cushioning.",
                        Price = 120.00M,
                        ProductImages = new List<ProductImage>()
                    },
                    new Product
                    {
                        Name = "LeBron 18",
                        Description = "Basketball shoes designed for maximum performance.",
                        Price = 200.00M,
                        ProductImages = new List<ProductImage>()
                    }
                };


                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

            if (!context.ProductImages.Any())
            {
                var products = await context.Products.ToListAsync();

                foreach(Product product in products)
                {
                    product.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = "https://example.com/image1.jpg",
                    });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}