using Microsoft.AspNetCore.Http;

namespace SneakerShop.SharedViewModel.Requests.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<IFormFile> ImageUrls { get; set; } = new List<IFormFile>();
    }
}