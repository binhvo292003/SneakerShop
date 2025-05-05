namespace SneakerShop.SharedViewModel.Requests.Product
{
    public class UpdateProductVariantRequest
    {
        public long Id { get; set; }
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}