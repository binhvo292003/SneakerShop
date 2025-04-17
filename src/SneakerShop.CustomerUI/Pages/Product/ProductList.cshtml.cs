using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.CustomerUI.Models;

namespace SneakerShop.CustomerUI.Pages.Product
{
    public class ProductList : PageModel
    {
        private readonly ILogger<ProductList> _logger;

        public List<ProductItem> Products { get; set; } = new List<ProductItem>();


        public ProductList(ILogger<ProductList> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Products = new List<ProductItem>
            {
                new ProductItem { Id = 1, Name = "Sneaker A", Slug="slug-a", Description = "Description A Description A Description A Description A Description A Description A Description A ", ImageUrl = "https://picsum.photos/300/200", Price = 99.99m },
                new ProductItem { Id = 2, Name = "Sneaker B", Slug="slug-b", Description = "Description B", ImageUrl = "https://picsum.photos/300/200", Price = 89.99m },
                new ProductItem { Id = 3, Name = "Sneaker C", Slug="slug-c", Description = "Description C", ImageUrl = "https://picsum.photos/300/200", Price = 79.99m },
                new ProductItem { Id = 4, Name = "Sneaker E", Slug="slug-d", Description = "Description E", ImageUrl = "https://picsum.photos/300/200", Price = 79.99m },
                new ProductItem { Id = 5, Name = "Sneaker F", Slug="slug-e", Description = "Description F", ImageUrl = "https://picsum.photos/300/200", Price = 79.99m }
            };
        }
    }
}