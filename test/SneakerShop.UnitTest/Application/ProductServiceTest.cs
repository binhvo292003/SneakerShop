using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using SneakerShop.Application.Common.Services;
using SneakerShop.Application.Settings;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Product;
using SneakerShop.SharedViewModel.Responses.Product;

namespace SneakerShop.UnitTest.Application
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IProductImageRepository> _productImageRepositoryMock;
        private readonly Mock<IProductVariantRepository> _productVariantRepositoryMock;
        private readonly ProductImageService _productImageService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productImageRepositoryMock = new Mock<IProductImageRepository>();
            _productVariantRepositoryMock = new Mock<IProductVariantRepository>();
            _mapperMock = new Mock<IMapper>();

            // Provide dummy Cloudinary settings for ProductImageService
            var config = Options.Create(new CloudinarySettings
            {
                CloudName = "test_cloud",
                ApiKey = "test_key",
                ApiSecret = "test_secret"
            });
            _productImageService = new ProductImageService(config, _productImageRepositoryMock.Object);

            _productService = new ProductService(
                _productRepositoryMock.Object,
                _productImageRepositoryMock.Object,
                _productVariantRepositoryMock.Object,
                _productImageService,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetAllProducts_ReturnsMappedResponses()
        {
            // Arrange
            var products = new List<Product> { new Product { Id = 1, Name = "A" }, new Product { Id = 2, Name = "B" } };
            var responses = new List<ProductResponse> { new ProductResponse { Id = 1, Name = "A" }, new ProductResponse { Id = 2, Name = "B" } };
            _productRepositoryMock.Setup(r => r.GetAllProducts()).ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProductResponse>>(products)).Returns(responses);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            Assert.Equal(responses, result);
        }

        [Fact]
        public async Task GetProductById_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _productRepositoryMock.Setup(r => r.GetProductById(5)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetProductById(5);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetProductById_ReturnsMappedResponse_WhenFound()
        {
            // Arrange
            var product = new Product { Id = 3, Name = "C" };
            var response = new ProductDetailResponse { Id = 3, Name = "C" };
            _productRepositoryMock.Setup(r => r.GetProductById(3)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductDetailResponse>(product)).Returns(response);

            // Act
            var result = await _productService.GetProductById(3);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task CreateProduct_CreatesAndReturnsResponse()
        {
            // Arrange
            var request = new CreateProductRequest { Name = "New", Description = "Desc", Price = 10m };
            var created = new Product { Id = 7, Name = "New", Description = "Desc", Price = 10m };
            _productRepositoryMock.Setup(r => r.CreateProduct(It.IsAny<Product>())).ReturnsAsync(created);

            // Act
            var result = await _productService.CreateProduct(request);

            // Assert
            Assert.Equal(created.Id, result.Id);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Description, result.Description);
            Assert.Equal(request.Price, result.Price);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var req = new UpdateProductRequest { Id = 99 };
            _productRepositoryMock.Setup(r => r.GetProductById(99)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.UpdateProduct(req);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsFalse_WhenNotFound()
        {
            // Arrange
            _productRepositoryMock.Setup(r => r.GetProductById(42)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.DeleteProduct(42);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsTrue_WhenExists()
        {
            // Arrange
            var existing = new Product { Id = 5 };
            _productRepositoryMock.Setup(r => r.GetProductById(5)).ReturnsAsync(existing);
            _productRepositoryMock.Setup(r => r.DeleteProduct(5)).ReturnsAsync(true);

            // Act
            var result = await _productService.DeleteProduct(5);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddProductVariant_ReturnsTrue_WhenCreated()
        {
            // Arrange
            var req = new AddProductVariantRequest { ProductId = 1, Size = "M", Stock = 5 };
            var createdVariant = new ProductVariant { Id = 10, ProductId = 1, Size = "M", Stock = 5 };
            _productVariantRepositoryMock.Setup(r => r.CreateProductVariant(It.IsAny<ProductVariant>())).ReturnsAsync(createdVariant);

            // Act
            var result = await _productService.AddProductVariant(req);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateStock_ReturnsFalse_WhenVariantNotFound()
        {
            // Arrange
            _productVariantRepositoryMock.Setup(r => r.GetProductVariantById(100)).ReturnsAsync((ProductVariant)null);

            // Act
            var result = await _productService.UpdateStock(100, 20);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateStock_ReturnsTrue_WhenUpdated()
        {
            // Arrange
            var variant = new ProductVariant { Id = 2, Stock = 1 };
            var updated = new ProductVariant { Id = 2, Stock = 20 };
            _productVariantRepositoryMock.Setup(r => r.GetProductVariantById(2)).ReturnsAsync(variant);
            _productVariantRepositoryMock.Setup(r => r.UpdateProductVariant(It.Is<ProductVariant>(v => v.Id == 2 && v.Stock == 20))).ReturnsAsync(updated);

            // Act
            var result = await _productService.UpdateStock(2, 20);

            // Assert
            Assert.True(result);
        }
    }
}
