using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(long id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        // soft delete product
        Task<bool> DeleteProduct(long id);
    }
}