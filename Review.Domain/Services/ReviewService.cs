using Reviews.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Reviews.Domain.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DatabaseContext databaseContext;

        public ReviewService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await databaseContext.Reviews.ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await databaseContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Review>> GetAllByProductIdAsync(Guid productId)
        {
            var rating = await databaseContext.Ratings.Include(x => x.Reviews).FirstOrDefaultAsync(x => x.ProductId == productId);
            return rating.Reviews.Where(x => x.Status == ReviewStatuses.Actual).ToList();
        }

        public async Task AddAsync(Review newReview, Guid productId)
        {
            var rating = await databaseContext.Ratings.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (rating == null)
            {
                rating = new Rating
                {
                    ProductId = productId,
                    CreateDate = DateTime.UtcNow
                };
            }
            var review = new Review
            {
                UserId = newReview.UserId,
                Text = newReview.Text,
                Grade = newReview.Grade,
                CreateDate = DateTime.UtcNow,
                RatingId = rating.Id,
                Status = ReviewStatuses.Actual
            };
            databaseContext.Reviews.Add(review);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<bool> TryToDeleteAsync(int id)
        {
            var review = await GetByIdAsync(id);
            if (review != null)
            {
                review.Status = ReviewStatuses.Deleted;
                await databaseContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
