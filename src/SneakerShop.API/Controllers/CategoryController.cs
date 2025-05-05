using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Mappings;
using SneakerShop.Application.Common.Services;
using SneakerShop.SharedViewModel.Requests.Category;

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
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var category = await _service.CreateCategory(request);
            return category is null ? NotFound() : Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest categoryRequest)
        {
            var result = await _service.UpdateCategory(categoryRequest);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _service.DeleteCategory(id);
            return result is false ? NotFound() : Ok(result);
        }
    }
}