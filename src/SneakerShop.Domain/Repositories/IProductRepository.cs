using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(long id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(long id);
        Task<Product> AddCategoryToProduct(long productId, long categoryId);
        Task<Product> RemoveCategoryFromProduct(long productId, long categoryId);
        Task<List<Product>> SearchProduct(string searchTerm, int page, int pageSize);
        Task<List<Product>> GetProductsByFilter(List<long> categoryIds, string sortBy, int page, int pageSize);
    }
}