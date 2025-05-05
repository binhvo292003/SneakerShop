using Microsoft.EntityFrameworkCore;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data;

namespace SneakerShop.Infrastructure.Repositories
{
    public class ProductImageRepository : BaseRepository, IProductImageRepository
    {
        public ProductImageRepository(StoreContext context) : base(context)
        {
        }

        public async Task<List<ProductImage>> GetAllProductImages()
        {
            return await _context.ProductImages.ToListAsync();
        }

        public async Task<ProductImage> GetProductImageById(long id)
        {
            return await _context.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task<ProductImage> CreateProductImage(ProductImage productImage)
        {
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }

        public async Task<bool> DeleteProductImage(long id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
                return false;

            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductImage>> GetAllProductImagesByProductId(long productId)
        {
            return await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductImage> UpdateProductImage(ProductImage productImage)
        {
            var existingProductImage = _context.ProductImages.Find(productImage.Id);
            if (existingProductImage == null)
                return null;


            _context.Entry(existingProductImage).CurrentValues.SetValues(productImage);

            await _context.SaveChangesAsync();
            return existingProductImage;
        }

        public async Task RemoveProductImagesByProductId(long productId)
        {
            var productImages = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();

            _context.ProductImages.RemoveRange(productImages);
            await _context.SaveChangesAsync();
        }


        public Task<ProductImage> FindProductImageByUrl(string publicId)
        {
            return _context.ProductImages.FirstOrDefaultAsync(pi => pi.PublicId == publicId);
        }
    }

}