namespace SneakerShop.Domain.Entities
{
    public class CartItem: BaseEntity
    {
        public long ProductVariantId { get; set; }
        public virtual required ProductVariant ProductVariant { get; set; }
        public long UserId { get; set; }
        public virtual required User User { get; set; }
        public int Quantity { get; set; }
    }
}