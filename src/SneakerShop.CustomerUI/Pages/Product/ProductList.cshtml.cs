using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.CustomerUI.Models;
using SneakerShop.CustomerUI.Services;
using SneakerShop.SharedViewModel.Responses.Category;

namespace SneakerShop.CustomerUI.Pages.Product
{
    public class ProductList : PageModel
    {
        private readonly ILogger<ProductList> _logger;

        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public List<ProductItem> Products { get; set; } = new List<ProductItem>();
        public List<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>(); 
        public ProductList(ILogger<ProductList> logger, ProductService productService, CategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task OnGetAsync([FromQuery] List<long> selectedCategories)
        {
            Categories = await _categoryService.GetAllCategoriesAsync();

            if (selectedCategories != null && selectedCategories.Any())
            {
                Products = await _productService.GetProductsByFilterAsync(selectedCategories);
            }
            else
            {
                Products = await _productService.GetAllProductsAsync();
            }
        }
    }
}