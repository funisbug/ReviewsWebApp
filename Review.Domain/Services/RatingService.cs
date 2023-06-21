using Microsoft.EntityFrameworkCore;
using Reviews.Domain.Models;

namespace Reviews.Domain.Services
{
    public class RatingService : IRatingService
    {
        private readonly DatabaseContext databaseContext;

        public RatingService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Rating>> GetRatingsAsync()
        {
            return await databaseContext.Ratings.ToListAsync();
        }        
    }
}
