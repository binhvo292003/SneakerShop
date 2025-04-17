using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.CustomerUI.Models;

namespace SneakerShop.CustomerUI.Pages.Product
{
    public class ProductDetail : PageModel
    {
        private readonly ILogger<ProductDetail> _logger;

        // Product to display
        public ProductItem Product { get; set; }

        public ProductDetail(ILogger<ProductDetail> logger)
        {
            _logger = logger;
        }

        public void OnGet(string slug)
        {
            // In a real app, you'd fetch this from a database or service
            // For now, we'll create mock data
            Product = GetProductBySlug(slug);
        }

        private ProductItem GetProductBySlug(string slug)
        {
            // Mock data - in a real app, this would come from a database
            var products = new List<ProductItem>
            {
                new ProductItem { Id = 1, Name = "Sneaker A", Slug = "slug-a", Description = "Description A", ImageUrl = "https://picsum.photos/300/200", Price = 99.99m },
                new ProductItem { Id = 2, Name = "Sneaker B", Slug = "slug-b", Description = "Description B", ImageUrl = "https://picsum.photos/300/200", Price = 89.99m },
                new ProductItem { Id = 3, Name = "Sneaker C", Slug = "slug-c", Description = "Description C", ImageUrl = "https://picsum.photos/300/200", Price = 79.99m }
            };

            var product = products.FirstOrDefault(p => p.Slug == slug) ??
                new ProductItem { Name = "Product Not Found", Description = "The requested product could not be found." };

            return product;
        }
    }
}