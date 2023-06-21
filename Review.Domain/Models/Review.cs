namespace Reviews.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string? Text { get; set; }

        public int Grade { get; set; }

        public DateTime CreateDate { get; set; }

        public int RatingId { get; set; }

        public Rating Rating { get; set; }

        public ReviewStatuses Status { get; set; }
    }
}