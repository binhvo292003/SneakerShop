using Microsoft.AspNetCore.Http;

namespace SneakerShop.SharedViewModel.Requests.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public List<int> Categories { get; set; } = new List<int>();
    }
}