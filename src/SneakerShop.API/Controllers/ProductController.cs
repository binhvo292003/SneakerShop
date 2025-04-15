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
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            return Ok(await _service.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _service.GetProductById(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO dto)
        {
            var created = await _service.CreateProduct(dto);
            return CreatedAtAction(nameof(GetAllProducts), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var result = await _service.UpdateProduct(dto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _service.DeleteProduct(id);
            return result ? NoContent() : NotFound();
        }
    }
}