using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using SneakerShop.Application.Common.Services;
using SneakerShop.Application.Settings;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.UnitTest.Application
{
    public class ProductImageTest
    {
        private readonly Mock<IProductImageRepository> _repositoryMock;
        private readonly ProductImageService _service;
        private readonly Mock<Cloudinary> _cloudinaryMock;

        public ProductImageTest()
        {
            _repositoryMock = new Mock<IProductImageRepository>();

            // Setup dummy CloudinarySettings
            var settings = new CloudinarySettings
            {
                CloudName = "test",
                ApiKey = "key",
                ApiSecret = "secret"
            };
            var options = Options.Create(settings);

            // Instantiate service (it will build its own Cloudinary internally)
            _service = new ProductImageService(options, _repositoryMock.Object);

            // Now create a Cloudinary mock using the same dummy account:
            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinaryMock = new Mock<Cloudinary>(account) { CallBase = false };

            // Replace the private _cloudinary field via reflection
            var field = typeof(ProductImageService)
                            .GetField("_cloudinary", BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(_service, _cloudinaryMock.Object);

        }

        [Fact]
        public async Task CreateProductImage_FileLengthZero_ReturnsNull()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(0);

            // Act
            var result = await _service.CreateProductImage(fileMock.Object, 1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateProductImage_UploadSuccess_ReturnsCreatedImage()
        {
            // Arrange
            var content = new byte[] { 1, 2, 3 };
            var stream = new MemoryStream(content);
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(content.Length);
            fileMock.Setup(f => f.FileName).Returns("test.jpg");
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

            var uploadResult = new ImageUploadResult
            {
                PublicId = "pub123",
                SecureUrl = new Uri("https://example.com/img.jpg"),
                Error = null
            };
            _cloudinaryMock
                    .Setup(c => c.UploadAsync(It.IsAny<ImageUploadParams>(), null))
                .ReturnsAsync(uploadResult);

            var saved = new ProductImage { Id = 1, PublicId = "pub123", ImageUrl = "https://example.com/img.jpg", ProductId = 2 };
            _repositoryMock
                .Setup(r => r.CreateProductImage(It.IsAny<ProductImage>()))
                .ReturnsAsync(saved);

            // Act
            var result = await _service.CreateProductImage(fileMock.Object, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(saved.Id, result.Id);
            Assert.Equal("pub123", result.PublicId);
            Assert.Equal("https://example.com/img.jpg", result.ImageUrl);
            Assert.Equal(2, result.ProductId);
            _repositoryMock.Verify(r => r.CreateProductImage(It.Is<ProductImage>(p => p.PublicId == "pub123" && p.ProductId == 2)), Times.Once);
        }

        [Fact]
        public async Task DeleteImage_NotFound_ReturnsNull()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetProductImageById(99)).ReturnsAsync((ProductImage)null);

            // Act
            var result = await _service.DeleteImage(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteImage_Found_DeletesAndReturnsResult()
        {
            // Arrange
            var existing = new ProductImage { Id = 1, PublicId = "pub123", ImageUrl = "url", ProductId = 5 };
            _repositoryMock.Setup(r => r.GetProductImageById(1)).ReturnsAsync(existing);

            var deletionResult = new DeletionResult { Result = "ok" };

            _cloudinaryMock
                .Setup(c => c.DestroyAsync(It.IsAny<DeletionParams>()))
                .ReturnsAsync(deletionResult);

            // Act
            var result = await _service.DeleteImage(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ok", result.Result);
            _repositoryMock.Verify(r => r.DeleteProductImage(1), Times.Once);
        }

        [Fact]
        public async Task GetProductImageByPublicId_NullOrEmpty_ReturnsNull()
        {
            // Act & Assert
            Assert.Null(await _service.GetProductImageByPublicId(null));
            Assert.Null(await _service.GetProductImageByPublicId(string.Empty));
        }

        [Fact]
        public async Task GetProductImageByPublicId_Found_ReturnsEntity()
        {
            // Arrange
            var image = new ProductImage { Id = 1, PublicId = "pub123" };
            _repositoryMock.Setup(r => r.FindProductImageByUrl("pub123")).ReturnsAsync(image);

            // Act
            var result = await _service.GetProductImageByPublicId("pub123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("pub123", result.PublicId);
        }
    }
}
