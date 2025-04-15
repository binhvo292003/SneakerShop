using SneakerShop.Domain.Entities;

namespace SneakerShop.Domain.Repositories
{
    public interface IReviewRepository
    {
        public Task<Review> GetReviewById(long id);
        public Task<List<Review>> GetAllReviewsByProductId(long productId);
        public Task<Review> CreateReview(Review review);
        public Task<Review> UpdateReview(Review review);
        // soft delete review
        public Task<bool> DeleteReview(long id);
    }
}