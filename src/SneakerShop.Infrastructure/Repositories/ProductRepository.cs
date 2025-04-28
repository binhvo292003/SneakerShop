using Microsoft.EntityFrameworkCore;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data;

namespace SneakerShop.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {

        public ProductRepository(StoreContext context) : base(context)
        {
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
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
                return null;

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Product> AddCategoryToProduct(long productId, long categoryId)
        {
            // add include(p => p.Categories) in order to load the categories relation
            var product = await _context.Products
                        .Include(p => p.Categories)
                        .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
                return null;

            var category = await _context.Categories
                        .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
                return null;

            if (product.Categories == null)
                product.Categories = new List<Category>();

            if (!product.Categories.Any(c => c.Id == categoryId))
                product.Categories.Add(category);

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> RemoveCategoryFromProduct(long productId, long categoryId)
        {
            var product = await _context.Products
                        .Include(p => p.Categories)
                        .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
                return null;

            var category = await _context.Categories
                        .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
                return null;

            if (product.Categories != null && product.Categories.Any(c => c.Id == categoryId))
                product.Categories.Remove(category);

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> SearchProduct(string searchTerm, int page, int pageSize)
        {
            return await _context.Products
                .Include(p => p.Categories)
                .Where(p => p.Name.Contains(searchTerm))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByFilter(List<long> categoryIds, string sortBy, int page, int pageSize)
        {
            return await _context.Products
                .Include(p => p.Categories)
                .Where(p => categoryIds.All(rcid => p.Categories.Any(c => c.Id == rcid)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }

}