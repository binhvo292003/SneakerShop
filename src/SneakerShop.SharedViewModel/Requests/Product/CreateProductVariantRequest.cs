namespace SneakerShop.SharedViewModel.Requests.Product
{
    public class CreateProductVariantRequest
    {
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public long ProductId { get; set; }
    }
}