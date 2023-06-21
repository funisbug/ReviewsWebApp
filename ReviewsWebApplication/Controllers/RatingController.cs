using Microsoft.AspNetCore.Mvc;
using Reviews.Domain.Models;
using Reviews.Domain.Services;

namespace ReviewsWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly ILogger<ReviewController> logger;
        private readonly IRatingService ratingService;

        public RatingController(ILogger<ReviewController> logger, IRatingService ratingService)
        {
            this.logger = logger;
            this.ratingService = ratingService;
        }

        [HttpGet("GetRating")]
        public async Task<ActionResult<List<Rating>>> GetRatingsAsync()
        {
            try
            {
                var result = await ratingService.GetRatingsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }
    }    
}
