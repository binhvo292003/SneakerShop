using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Mappings;
using SneakerShop.Application.Common.Services;
using SneakerShop.SharedViewModel.Requests.Product;

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
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var created = await _service.CreateProduct(request);
            return created is null ? NotFound() : Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest request)
        {
            var result = await _service.UpdateProduct(request);
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct(string searchTerm, int page = 1, int pageSize = 10)
        {
            var result = await _service.SearchProduct(searchTerm, page, pageSize);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetProductsByFilter([FromQuery] string categoryQuery = null , [FromQuery] string sortBy = null, int page = 1, int pageSize = 10)
        {
            List<long> categoryIds = new List<long>();
            if (!string.IsNullOrEmpty(categoryQuery))
            {
                var ids = categoryQuery.Split(',');
                foreach (var id in ids)
                {
                    if (long.TryParse(id, out long categoryId))
                    {
                        categoryIds.Add(categoryId);
                    }
                }
            }

            var result = await _service.GetProductsByFilter(categoryIds, sortBy, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{productId}/variants")]
        public async Task<IActionResult> GetProductVariants(long productId)
        {
            var result = await _service.GetProductVariants(productId);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("{productId}/variants")]
        public async Task<IActionResult> AddProductVariant([FromBody] AddProductVariantRequest request)
        {
            var result = await _service.AddProductVariant(request);
            return result is false ? NotFound() : Ok(result);
        }

        

    }
}