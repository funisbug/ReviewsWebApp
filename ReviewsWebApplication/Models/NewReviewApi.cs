using System.ComponentModel.DataAnnotations;

namespace ReviewsWebApplication.Models
{
    public class NewReviewApi
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        public string? Text { get; set; }

        [Required]
        [Range(0, 5)]
        public int Grade { get; set; }
    }
}
