namespace ReviewsWebApplication.Models
{
    public class RatingApi
    {
        public int Id { get; set; }

        public Guid ProductId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
