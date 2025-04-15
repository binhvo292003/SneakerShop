using SneakerShop.Domain.Enums;

namespace SneakerShop.Domain.Entities
{
    public class Order : BaseEntity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public float Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}