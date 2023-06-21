namespace Reviews.Domain.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public Guid ProductId { get; set; }

        public DateTime CreateDate { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
