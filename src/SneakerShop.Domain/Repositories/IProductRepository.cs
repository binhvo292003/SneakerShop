using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(long id);
        public Task<Product> CreateProduct(Product product);
        public Task<Product> UpdateProduct(Product product);
        public Task<bool> DeleteProduct(long id);
        public Task<Product> AddCategoryToProduct(long productId, long categoryId);
        public Task<Product> RemoveCategoryFromProduct(long productId, long categoryId);
        public Task<List<Product>> SearchProduct(string searchTerm, int page, int pageSize);
        public Task<List<Product>> GetProductsByFilter(List<long> categoryIds, string sortBy, int page, int pageSize);
    }
}