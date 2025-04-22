using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Review;

namespace SneakerShop.Application.Common.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByProductId(long productId)
        {
            var reviews = await _reviewRepository.GetAllReviewsByProductId(productId);
            return reviews.ToList();
        }

        public async Task<bool> CreateReview(CreateReviewRequest request)
        {
            if(request.Rating < 1 || request.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            var review = new Review
            {
                ProductId = request.ProductId,
                UserId = request.UserId,
                Comment = request.Comment,
                Rating = request.Rating
            };
            var result = await _reviewRepository.CreateReview(review);
            return result != null;
        }

    }
}