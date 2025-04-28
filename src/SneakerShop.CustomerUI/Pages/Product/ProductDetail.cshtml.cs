using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.CustomerUI.Models;
using SneakerShop.CustomerUI.Services;
using SneakerShop.SharedViewModel.Responses.Product;

namespace SneakerShop.CustomerUI.Pages.Product
{
    public class ProductDetail : PageModel
    {
        private readonly ILogger<ProductDetail> _logger;
        private readonly ProductService _productService;

        public ProductDetailResponse Product { get; set; } = new ProductDetailResponse();

        public ProductDetail(ILogger<ProductDetail> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task OnGetAsync(string slug)
        {
            var product = await _productService.GetProductByIdAsync(Int32.Parse(slug));
            Product = product ?? new ProductDetailResponse();
            // Console.WriteLine(product);
            // if(product == null)
            // {
            //     return NotFound();
            // }
        }
    }
}