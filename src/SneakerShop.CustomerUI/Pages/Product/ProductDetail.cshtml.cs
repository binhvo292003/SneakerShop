using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.CustomerUI.Models;
using SneakerShop.CustomerUI.Services;
using SneakerShop.SharedViewModel.Requests.Review;
using SneakerShop.SharedViewModel.Responses.Product;
using SneakerShop.SharedViewModel.Responses.Review;

namespace SneakerShop.CustomerUI.Pages.Product
{
    public class ProductDetail : PageModel
    {
        private readonly ILogger<ProductDetail> _logger;
        private readonly ProductService _productService;

        public ProductDetailResponse Product { get; set; } = new ProductDetailResponse();
        [BindProperty]
        public CreateReviewRequest UserReview { get; set; } = new CreateReviewRequest();
        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();

        public ProductDetail(ILogger<ProductDetail> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            var product = await _productService.GetProductByIdAsync(Int32.Parse(slug));
            Product = product ?? new ProductDetailResponse();

            if (product == null)
            {
                return RedirectToPage("/Error");
            }

            Reviews = await _productService.GetReviewsByProductIdAsync(Int32.Parse(slug));

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(string slug)
        {
            if (!ModelState.IsValid)
            {
                Product = await _productService.GetProductByIdAsync(int.Parse(slug));
                return Page();
            }

            UserReview.ProductId = int.Parse(slug);
            UserReview.UserId = long.Parse(Request.Cookies["UserId"]);

            var response = await _productService.SubmitReviewAsync(UserReview);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage(new { slug });
            }
            else
            {
                ModelState.AddModelError("", "Sorry, we couldn’t save your review. Please try again.");
                Product = await _productService.GetProductByIdAsync(int.Parse(slug));
                return Page();
            }
        }
    }
}