namespace SneakerShop.SharedViewModel.Responses.Product
{
    public class ProductDetailResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public List<ProductVariantResponse> ProductVariants { get; set; } = new List<ProductVariantResponse>();
    }
}