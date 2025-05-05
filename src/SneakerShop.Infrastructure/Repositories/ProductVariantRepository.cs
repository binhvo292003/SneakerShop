using Microsoft.EntityFrameworkCore;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data;

namespace SneakerShop.Infrastructure.Repositories
{
    public class ProductVariantRepository : BaseRepository, IProductVariantRepository
    {
        public ProductVariantRepository(StoreContext context) : base(context)
        {
        }

        public async Task<ProductVariant> CreateProductVariant(ProductVariant productVariant)
        {
            await _context.ProductVariants.AddAsync(productVariant);
            await _context.SaveChangesAsync();
            return productVariant;
        }

        public async Task<bool> DeleteProductVariant(long id)
        {
            var productVariant = await _context.ProductVariants.FindAsync(id);
            if (productVariant == null)
                return false;
            
            _context.ProductVariants.Remove(productVariant);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<List<ProductVariant>> GetAllProductVariants()
        {
            var productVariants = _context.ProductVariants
                .Include(pv => pv.Product)
                .Include(pv => pv.Size)
                .ToListAsync();
            return productVariants;
        }

        public Task<ProductVariant> GetProductVariantById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductVariant>> GetProductVariantsByProductId(long productId)
        {
            var productVariants = _context.ProductVariants
                .Where(pv => pv.ProductId == productId)
                .ToListAsync();
            return productVariants;
        }

        public async Task<ProductVariant> UpdateProductVariant(ProductVariant productVariant)
        {
            var existingProductVariant = await _context.ProductVariants.FindAsync(productVariant.Id);
            if (existingProductVariant == null)
                return null;

            _context.Entry(existingProductVariant).CurrentValues.SetValues(productVariant);
            await _context.SaveChangesAsync();
            return existingProductVariant;
        }
        
    }
}