namespace SneakerShop.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}