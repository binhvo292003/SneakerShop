using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.CustomerUI.Models;

namespace SneakerShop.CustomerUI.Pages.Home
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;

        public List<CarouselItem> CarouselItems { get; set; }
        public new IFormFile File { get; set; }

        public Index(ILogger<Index> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Initialize carousel data
            CarouselItems = new List<CarouselItem>
            {
                new CarouselItem
                {
                    ImageUrl = "https://picsum.photos/id/1018/1000/600",
                    AltText = "Featured Sneakers",
                    Title = "New Arrivals",
                    Description = "Check out our latest collection"
                },
                new CarouselItem
                {
                    ImageUrl = "https://picsum.photos/id/1018/1000/600",
                    AltText = "Limited Edition",
                    Title = "Limited Edition Series",
                    Description = "Exclusive designs available for a limited time"
                },
                new CarouselItem
                {
                    ImageUrl = "https://picsum.photos/id/1018/1000/600",
                    AltText = "Sale Items",
                    Title = "Season End Sale",
                    Description = "Up to 50% off on selected items"
                },
                new CarouselItem
                {
                    ImageUrl = "https://picsum.photos/id/1018/1000/600",
                    AltText = "Premium Collection",
                    Title = "Premium Collection",
                    Description = "Luxury sneakers for the discerning customer"
                },
                new CarouselItem
                {
                    ImageUrl = "https://picsum.photos/id/1018/1000/600",
                    AltText = "Sports Collection",
                    Title = "Performance Series",
                    Description = "Designed for maximum performance"
                }
            };
        }
    }

}