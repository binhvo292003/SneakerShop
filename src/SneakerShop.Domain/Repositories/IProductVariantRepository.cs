using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetAllProductVariants();
        Task<ProductVariant> GetProductVariantById(long id);
        Task<ProductVariant> CreateProductVariant(ProductVariant productVariant);
        Task<ProductVariant> UpdateProductVariant(ProductVariant productVariant);
        // soft delete product variant
        Task<bool> DeleteProductVariant(long id);
    }
}