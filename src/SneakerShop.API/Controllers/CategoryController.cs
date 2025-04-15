using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Mappings;
using SneakerShop.Application.Common.Services;

namespace SneakerShop.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _service.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO dto)
        {
            var category = await _service.CreateCategory(dto);
            return category is null ? NotFound() : Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO dto)
        {
            var result = await _service.UpdateCategory(dto);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _service.DeleteCategory(id);
            return result is false? NotFound() : Ok(result);
        }
    }
}