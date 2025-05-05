namespace SneakerShop.CustomerUI.Models
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public int Stock { get; set; }
    }
}