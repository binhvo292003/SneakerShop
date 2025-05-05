namespace SneakerShop.CustomerUI.Models
{
    public class ProductItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Slug { get; set; }
    }
}