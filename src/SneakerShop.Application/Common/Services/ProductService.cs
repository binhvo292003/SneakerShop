using SneakerShop.Application.Common.Mappings;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;

namespace SneakerShop.Application.Common.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _repository.GetAllProducts();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            });
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var product = await _repository.GetProductById(id);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            var result = await _repository.CreateProduct(product);

            dto.Id = result.Id;
            return dto;
        }

        public async Task<bool> UpdateProduct(ProductDTO dto)
        {
            var existing = await _repository.GetProductById(dto.Id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;

            await _repository.UpdateProduct(existing);
            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var existing = await _repository.GetProductById(id);
            if (existing == null) return false;

            await _repository.DeleteProduct(id);
            return true;
        }
    }
}