using Microsoft.EntityFrameworkCore;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;

namespace SneakerShop.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Categories)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(long id)
        {
            return await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
                return null;

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}