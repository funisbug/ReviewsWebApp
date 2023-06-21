using Reviews.Domain.Models;

namespace Reviews.Domain.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllReviewsAsync();

        Task<Review> GetByIdAsync(int id);

        Task<List<Review>> GetAllByProductIdAsync(Guid productId);

        Task AddAsync(Review newReview, Guid productId);

        Task<bool> TryToDeleteAsync(int id);
    }
}
