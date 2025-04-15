using Microsoft.EntityFrameworkCore;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data;

namespace SneakerShop.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContext _context;
        public CategoryRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(long id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            
            return category;
        }

        public async Task<bool> DeleteCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var updateCategory = await _context.Categories.FindAsync(category.Id);
            if (updateCategory == null)
                return null;

            _context.Entry(updateCategory).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();
            return updateCategory;
        }
    }
}