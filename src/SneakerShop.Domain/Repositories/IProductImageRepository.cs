using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface IProductImageRepository
    {
        public Task<ProductImage> GetProductImageById(long id);
        public Task<List<ProductImage>> GetAllProductImagesByProductId(long productId);
        public Task<ProductImage> CreateProductImage(ProductImage productImage);
        public Task<ProductImage> UpdateProductImage(ProductImage productImage);
        // soft delete product image
        public Task<bool> DeleteProductImage(long id);
    }
}