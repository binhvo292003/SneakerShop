using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.CustomerUI.Models;
using SneakerShop.CustomerUI.Services;

namespace SneakerShop.CustomerUI.Pages.Product
{
    public class ProductList : PageModel
    {
        private readonly ILogger<ProductList> _logger;

        private readonly ProductService _productService;

        public List<ProductItem> Products { get; set; } = new List<ProductItem>();

        public ProductList(ILogger<ProductList> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task OnGetAsync()
        {
            Products = await _productService.GetAllProductsAsync();
        }
    }
}