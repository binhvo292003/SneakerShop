using SneakerShop.Application.Common.Mappings;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;

namespace SneakerShop.Application.Common.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await _repository.GetAllCategories();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
            });
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var category = await _repository.GetCategoryById(id);
            if (category == null) return null;

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
            };

            var result = await _repository.CreateCategory(category);

            dto.Id = result.Id;
            return dto;
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO dto)
        {
            var existing = await _repository.GetCategoryById(dto.Id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            await _repository.UpdateCategory(existing);
            return dto;
        }

        public async Task<bool> DeleteCategory(long id)
        {
            var existing = await _repository.GetCategoryById(id);
            if (existing == null) return false;

            await _repository.DeleteCategory(id);
            return true;
        }
    }
}