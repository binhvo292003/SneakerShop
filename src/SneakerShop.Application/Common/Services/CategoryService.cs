using AutoMapper;
using SneakerShop.Application.Common.Mappings;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Category;
using SneakerShop.SharedViewModel.Responses.Category;

namespace SneakerShop.Application.Common.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
            };

            var newCategory = await _repository.CreateCategory(category);

            var result = _mapper.Map<CategoryResponse>(newCategory);
            return result;
        }

        public async Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest request)
        {
            var existing = await _repository.GetCategoryById(request.Id);
            if (existing == null) return null;

            existing.Name = request.Name;

            var editCategory = await _repository.UpdateCategory(existing);
            var result = _mapper.Map<CategoryResponse>(editCategory);

            return result;
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