namespace SneakerShop.Domain.Entities
{
    public class User
    {
        public required string Email { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}