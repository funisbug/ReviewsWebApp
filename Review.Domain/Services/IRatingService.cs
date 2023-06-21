using Reviews.Domain.Models;

namespace Reviews.Domain.Services
{
    public interface IRatingService
    {
        Task<List<Rating>> GetRatingsAsync();
    }
}
