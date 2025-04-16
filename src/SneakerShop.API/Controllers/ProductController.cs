using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Mappings;
using SneakerShop.Application.Common.Services;

namespace SneakerShop.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _service.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductById(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO dto)
        {
            var created = await _service.CreateProduct(dto);
            return created is null ? NotFound() : Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO dto)
        {
            var result = await _service.UpdateProduct(dto);
            return result is null ? NoContent() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _service.DeleteProduct(id);
            return result ? NoContent() : NotFound(result);
        }

        [HttpPost("{productId}/categories/{categoryId}")]
        public async Task<IActionResult> AddCategoryToProduct(long productId, long categoryId)
        {
            var result = await _service.AddCategoryToProduct(productId, categoryId);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{productId}/categories/{categoryId}")]
        public async Task<IActionResult> RemoveCategoryFromProduct(long productId, long categoryId)
        {
            var result = await _service.RemoveCategoryFromProduct(productId, categoryId);
            return result is null ? NotFound() : Ok(result);
        }
    }
}