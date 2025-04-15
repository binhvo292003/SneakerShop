namespace SneakerShop.Domain.Entities
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}