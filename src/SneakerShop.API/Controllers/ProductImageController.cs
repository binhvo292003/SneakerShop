using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Services;

namespace SneakerShop.API.Controllers
{
    [ApiController]
    [Route("api/product-images")]
    public class ProductImageController:ControllerBase
    {
        private readonly ProductImageService _service;

        public ProductImageController(ProductImageService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(IFormFile request, long productId)
        {
            var created = await _service.CreateProductImage(request, productId);
            if (created == null)
            {
                return BadRequest("Image upload failed.");
            }

            return created is null ? NotFound() : Ok(created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(long id)
        {
            var result = await _service.DeleteImage(id);

            return result is null ? NotFound() : Ok(result);
        }
    }
}