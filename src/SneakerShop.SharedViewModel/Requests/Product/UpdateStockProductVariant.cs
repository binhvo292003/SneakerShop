namespace SneakerShop.SharedViewModel.Requests.Product
{
    public class UpdateStockProductVariant
    {
        public long ProductVariantId { get; set; }
        public int Stock { get; set; } = 0;
    }
}