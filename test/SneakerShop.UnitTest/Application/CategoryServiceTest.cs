using AutoMapper;
using Moq;
using SneakerShop.Application.Common.Services;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Category;
using SneakerShop.SharedViewModel.Responses.Category;

namespace SneakerShop.UnitTest.Application
{
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CategoryService _service;

        public CategoryServiceTest()
        {
            _repositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new CategoryService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllCategories_ReturnsMappedList()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Sneakers" },
                new Category { Id = 2, Name = "Boots" }
            };
            _repositoryMock
                .Setup(r => r.GetAllCategories())
                .ReturnsAsync(categories);

            // Act
            var result = await _service.GetAllCategories();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Id == 1 && c.Name == "Sneakers");
            Assert.Contains(result, c => c.Id == 2 && c.Name == "Boots");
        }

        [Fact]
        public async Task GetCategoryById_WhenExists_ReturnsDto()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Sneakers" };
            _repositoryMock
                .Setup(r => r.GetCategoryById(1))
                .ReturnsAsync(category);

            // Act
            var result = await _service.GetCategoryById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Sneakers", result.Name);
        }

        [Fact]
        public async Task GetCategoryById_WhenNotExists_ReturnsNull()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetCategoryById(99))
                .ReturnsAsync((Category)null);

            // Act
            var result = await _service.GetCategoryById(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateCategory_ReturnsMappedResponse()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "Sneakers" };
            var created = new Category { Id = 1, Name = "Sneakers" };
            var mapped = new CategoryResponse { Id = 1, Name = "Sneakers" };

            _repositoryMock
                .Setup(r => r.CreateCategory(It.IsAny<Category>()))
                .ReturnsAsync(created);
            _mapperMock
                .Setup(m => m.Map<CategoryResponse>(created))
                .Returns(mapped);

            // Act
            var result = await _service.CreateCategory(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mapped.Id, result.Id);
            Assert.Equal(mapped.Name, result.Name);
            _repositoryMock.Verify(r => r.CreateCategory(It.Is<Category>(c => c.Name == "Sneakers")), Times.Once);
            _mapperMock.Verify(m => m.Map<CategoryResponse>(created), Times.Once);
        }

        [Fact]
        public async Task UpdateCategory_WhenNotExists_ReturnsNull()
        {
            // Arrange
            var request = new UpdateCategoryRequest { Id = 99, Name = "Updated" };
            _repositoryMock
                .Setup(r => r.GetCategoryById(99))
                .ReturnsAsync((Category)null);

            // Act
            var result = await _service.UpdateCategory(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateCategory_WhenExists_ReturnsMappedResponse()
        {
            // Arrange
            var existing = new Category { Id = 1, Name = "Old" };
            var request = new UpdateCategoryRequest { Id = 1, Name = "New" };
            var updated = new Category { Id = 1, Name = "New" };
            var mapped = new CategoryResponse { Id = 1, Name = "New" };

            _repositoryMock.Setup(r => r.GetCategoryById(1)).ReturnsAsync(existing);
            _repositoryMock.Setup(r => r.UpdateCategory(existing)).ReturnsAsync(updated);
            _mapperMock.Setup(m => m.Map<CategoryResponse>(updated)).Returns(mapped);

            // Act
            var result = await _service.UpdateCategory(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New", result.Name);
            _repositoryMock.Verify(r => r.UpdateCategory(It.Is<Category>(c => c.Id == 1 && c.Name == "New")), Times.Once);
            _mapperMock.Verify(m => m.Map<CategoryResponse>(updated), Times.Once);
        }

        [Fact]
        public async Task DeleteCategory_WhenNotExists_ReturnsFalse()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetCategoryById(99))
                .ReturnsAsync((Category)null);

            // Act
            var result = await _service.DeleteCategory(99);

            // Assert
            Assert.False(result);
            _repositoryMock.Verify(r => r.DeleteCategory(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCategory_WhenExists_DeletesAndReturnsTrue()
        {
            // Arrange
            var existing = new Category { Id = 1, Name = "Sneakers" };
            _repositoryMock.Setup(r => r.GetCategoryById(1)).ReturnsAsync(existing);

            // Act
            var result = await _service.DeleteCategory(1);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.DeleteCategory(1), Times.Once);
        }

    }
}