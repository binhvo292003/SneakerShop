using AutoMapper;
using Moq;
using SneakerShop.Application.Common.Services;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Review;
using SneakerShop.SharedViewModel.Responses.Review;

namespace SneakerShop.UnitTest.Application
{
    public class ReviewServiceTest
    {
        private readonly Mock<IReviewRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ReviewService _service;

        public ReviewServiceTest()
        {
            _repositoryMock = new Mock<IReviewRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ReviewService(_repositoryMock.Object, _mapperMock.Object);
        }
        [Fact]
        public async Task GetAllReviewsByProductId_ReturnsMappedList()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, ProductId = 1, UserId = 2, Comment = "Great!", Rating = 5 },
                new Review { Id = 2, ProductId = 1, UserId = 3, Comment = "Bad", Rating = 1 }
            };
            var mapped = new List<ReviewResponse>
            {
                new ReviewResponse { Id = 1, Comment = "Great!", Rating = 5 },
                new ReviewResponse { Id = 2, Comment = "Bad", Rating = 1 }
            };
            _repositoryMock
                .Setup(r => r.GetAllReviewsByProductId(1))
                .ReturnsAsync(reviews);
            _mapperMock
                .Setup(m => m.Map<IEnumerable<ReviewResponse>>(reviews))
                .Returns(mapped);

            // Act
            var result = await _service.GetAllReviewsByProductId(1);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Id == 1 && r.Comment == "Great!");
            Assert.Contains(result, r => r.Id == 2 && r.Comment == "Bad");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        public async Task CreateReview_InvalidRating_ThrowsArgumentException(int rating)
        {
            // Arrange
            var request = new CreateReviewRequest { ProductId = 1, UserId = 1, Rating = rating, Comment = "Test" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateReview(request));
        }

        [Fact]
        public async Task CreateReview_ValidRequest_ReturnsTrue_WhenRepositorySucceeds()
        {
            // Arrange
            var request = new CreateReviewRequest { ProductId = 1, UserId = 1, Rating = 4, Comment = "Nice" };
            var saved = new Review { ProductId = 1, UserId = 1, Comment = "Nice", Rating = 4 };
            _repositoryMock
                .Setup(r => r.CreateReview(It.IsAny<Review>()))
                .ReturnsAsync(saved);

            // Act
            var result = await _service.CreateReview(request);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.CreateReview(It.Is<Review>(rev =>
                rev.ProductId == request.ProductId &&
                rev.UserId == request.UserId &&
                rev.Rating == request.Rating &&
                rev.Comment == request.Comment
            )), Times.Once);
        }

        [Fact]
        public async Task CreateReview_ValidRequest_ReturnsFalse_WhenRepositoryReturnsNull()
        {
            // Arrange
            var request = new CreateReviewRequest { ProductId = 1, UserId = 1, Rating = 3, Comment = "OK" };
            _repositoryMock
                .Setup(r => r.CreateReview(It.IsAny<Review>()))
                .ReturnsAsync((Review)null);

            // Act
            var result = await _service.CreateReview(request);

            // Assert
            Assert.False(result);
        }
    }
}
