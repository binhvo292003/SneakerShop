using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface ICategoryRepository
    {
        public Task<Category> GetCategoryById(long id);
        public Task<List<Category>> GetAllCategories();
        public Task<Category> CreateCategory(Category category);
        public Task<Category> UpdateCategory(Category category);
        // soft delete category
        public Task<bool> DeleteCategory(long id);
    }
}