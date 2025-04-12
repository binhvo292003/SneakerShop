namespace SneakerShop.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public long ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public virtual required Product Product { get; set; }
    }
}