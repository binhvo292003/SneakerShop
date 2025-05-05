namespace SneakerShop.SharedViewModel.Requests.Product
{
    public class AddProductVariantRequest
    {
        public long ProductId { get; set; }
        public string Size { get; set; } = null!;
        public int Stock { get; set; }
    }
}