using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data;

namespace SneakerShop.Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(StoreContext context) : base(context)
        {
        }
        public async Task<Review> CreateReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public Task<bool> DeleteReview(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviewsByProductId(long productId)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}