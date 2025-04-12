namespace SneakerShop.Domain.Entities
{
    public class Category: BaseEntity
    {
        public required string Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}