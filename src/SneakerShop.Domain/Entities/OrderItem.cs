namespace SneakerShop.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        public long ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}