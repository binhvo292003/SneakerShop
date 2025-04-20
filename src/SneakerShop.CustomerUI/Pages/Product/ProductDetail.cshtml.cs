using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.CustomerUI.Models;
using SneakerShop.CustomerUI.Services;

namespace SneakerShop.CustomerUI.Pages.Product
{
    public class ProductDetail : PageModel
    {
        private readonly ILogger<ProductDetail> _logger;
        private readonly ProductService _productService;

        public ProductItem Product { get; set; } = new ProductItem();

        public ProductDetail(ILogger<ProductDetail> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            var products = await _productService.GetAllProductsAsync();
            Product = products.FirstOrDefault(p => p.Slug == slug) ??
                new ProductItem
                {
                    Name = "Product Not Found",
                    Description = "The requested product could not be found."
                };

            return Page();
        }
    }
}