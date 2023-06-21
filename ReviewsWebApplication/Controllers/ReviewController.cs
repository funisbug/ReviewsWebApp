using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reviews.Domain.Models;
using Reviews.Domain.Services;
using ReviewsWebApplication.Models;

namespace ReviewsWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {

        private readonly ILogger<ReviewController> logger;
        private readonly IReviewService reviewService;
        private readonly IMapper mapper;

        public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService, IMapper mapper)
        {
            this.logger = logger;
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Review>>> GetAllReviewsAsync()
        {
            try
            {
                var reviews = await reviewService.GetAllReviewsAsync();
                return Ok(mapper.Map<List<ReviewApi>>(reviews));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Review>> GetByIdAsync(int id)
        {
            try
            {
                var review = await reviewService.GetByIdAsync(id);
                if (review != null)
                {
                    return Ok(mapper.Map<ReviewApi>(review));
                }
                return BadRequest("Invalid review Id");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }

        [HttpGet("GetByProductId")]
        public async Task<ActionResult<List<Review>>> GetByProductIdAsync(Guid productId)
        {
            try
            {
                var reviews = await reviewService.GetAllByProductIdAsync(productId);
                if (reviews != null)
                {
                    return Ok(mapper.Map<List<ReviewApi>>(reviews));
                }
                return BadRequest("Invalid product Id");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }

        
        [HttpPost("AddReview")]
        public async Task<ActionResult> AddAsync([FromBody] NewReviewApi newReview)
        {
            try
            {
                await reviewService.AddAsync(mapper.Map<Review>(newReview), newReview.ProductId);
                return Ok("Review added");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await reviewService.TryToDeleteAsync(id);
                if (result)
                {
                    return Ok("Review removed");
                }                    
                return BadRequest("Invalid product Id");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }
    }
}