namespace SneakerShop.CustomerUI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Username { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } 
        public long ProductId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}