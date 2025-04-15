namespace SneakerShop.Domain.Entities
{
    public class CartItem: BaseEntity
    {
        public long ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public int Quantity { get; set; }
    }
}