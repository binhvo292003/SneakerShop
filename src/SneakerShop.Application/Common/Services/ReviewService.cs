using AutoMapper;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Review;
using SneakerShop.SharedViewModel.Responses.Review;

namespace SneakerShop.Application.Common.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewResponse>> GetAllReviewsByProductId(long productId)
        {
            var reviews = await _reviewRepository.GetAllReviewsByProductId(productId);

            var result = _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
            return result.ToList();
        }

        public async Task<bool> CreateReview(CreateReviewRequest request)
        {
            if (request.Rating < 1 || request.Rating > 5)
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