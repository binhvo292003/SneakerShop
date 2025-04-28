using AutoMapper;
using SneakerShop.Application.Common.Mappings;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Product;
using SneakerShop.SharedViewModel.Responses.Product;

namespace SneakerShop.Application.Common.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ProductImageService _productImageService;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
                        IProductImageRepository productImageRepository,
                        IProductVariantRepository productVariantRepository,
                        ProductImageService productImageService,
                        IMapper mapper)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _productVariantRepository = productVariantRepository;
            _productImageService = productImageService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();

            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return result;
        }

        public async Task<ProductDetailResponse> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null) return null;

            return _mapper.Map<ProductDetailResponse>(product);
        }

        public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };


            var result = await _productRepository.CreateProduct(product);

            var pr = new ProductResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price

            };

            if (request.ImageUrls != null)
            {
                var imageUrls = new List<ProductImage>();
                foreach (var imageUrl in request.ImageUrls)
                {
                    await _productImageService.CreateProductImage(imageUrl, pr.Id);
                }
                pr.ImageUrl = imageUrls.Select(i => i.ImageUrl).FirstOrDefault();
            }

            return pr;
        }

        public async Task<ProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            var existingProduct = await _productRepository.GetProductById(request.Id);
            if (existingProduct == null) return null;

            await _productImageRepository.RemoveProductImagesByProductId(existingProduct.Id);

            var newImageUrls = new List<string>();

            if (request.ImageUrls != null && request.ImageUrls.Any())
            {
                foreach (var file in request.ImageUrls)
                {
                    var productImage = await _productImageService.GetProductImageByPublicId(file);
                    if (productImage != null)
                    {
                        productImage.ProductId = existingProduct.Id;
                        await _productImageRepository.UpdateProductImage(productImage);
                        newImageUrls.Add(productImage.ImageUrl);
                    }
                }
            }

            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;

            var updated = await _productRepository.UpdateProduct(existingProduct);

            return new ProductResponse
            {
                Id = updated.Id,
                Name = updated.Name,
                Description = updated.Description,
                Price = updated.Price,
                ImageUrl = newImageUrls.FirstOrDefault()
            };
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var existing = await _productRepository.GetProductById(id);
            if (existing == null) return false;

            await _productRepository.DeleteProduct(id);
            return true;
        }
        public async Task<ProductDTO> AddCategoryToProduct(long productId, long categoryId)
        {
            var product = await _productRepository.AddCategoryToProduct(productId, categoryId);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<ProductDTO> RemoveCategoryFromProduct(long productId, long categoryId)
        {
            var product = await _productRepository.RemoveCategoryFromProduct(productId, categoryId);
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<List<ProductDTO>> SearchProduct(string searchTerm, int page, int pageSize)
        {
            var products = await _productRepository.SearchProduct(searchTerm, page, pageSize);
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();
        }

        public async Task<List<ProductDTO>> GetProductsByFilter(List<long> categoryIds, string sortBy = null, int page = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetProductsByFilter(categoryIds, sortBy, page, pageSize);
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();
        }

        public async Task<bool> AddProductVariant(AddProductVariantRequest request)
        {
            var productVariant = new ProductVariant
            {
                Size = request.Size,
                Stock = request.Stock,
                ProductId = request.ProductId
            };
            var result = await _productVariantRepository.CreateProductVariant(productVariant);

            return result != null;
        }

        public async Task<List<ProductVariantResponse>> GetProductVariants(long productId)
        {
            var productVariants = await _productVariantRepository.GetProductVariantsByProductId(productId);
            return _mapper.Map<List<ProductVariantResponse>>(productVariants);
        }

        public async Task<bool> UpdateStock(long productVariantId, int stock)
        {
            var productVariant = await _productVariantRepository.GetProductVariantById(productVariantId);
            if (productVariant == null) return false;

            productVariant.Stock = stock;
            var result = await _productVariantRepository.UpdateProductVariant(productVariant);

            return result != null;
        }
    }
}