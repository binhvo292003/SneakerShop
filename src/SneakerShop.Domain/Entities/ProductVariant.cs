namespace SneakerShop.Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        public string Size { get; set; }
        public int Stock { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = null!;
    }
}