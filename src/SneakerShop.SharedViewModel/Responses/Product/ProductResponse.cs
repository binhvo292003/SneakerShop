namespace SneakerShop.SharedViewModel.Responses.Product
{
    public class ProductResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<string> ImageUrl { get; set; }
        public long CategoryId { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}