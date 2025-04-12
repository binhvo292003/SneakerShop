namespace SneakerShop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
        public virtual ICollection<ProductVariant>? ProductVariants { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; } = null!;
    }

}