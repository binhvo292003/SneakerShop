namespace SneakerShop.SharedViewModel.Requests.Product
{
    public class UpdateProductRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public List<long> CatetegoryIds { get; set; } = new List<long>();
    }
}