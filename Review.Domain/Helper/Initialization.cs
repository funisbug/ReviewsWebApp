using Reviews.Domain.Models;

namespace Reviews.Domain.Helper
{
    public static class Initialization
    {
        private static Random random = new Random();
        private static int reviewId = 1;
        public static List<Review> Reviews = new List<Review>();
        private const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        private static List<string> productId = new List<string> { "03b0f5f5-1690-40ea-8434-f3a4262eec2a", "3b9a2b9b-70d0-41f7-80be-f1d028a19ce7", "528f8578-a586-4b7b-855e-85dc9aad4423", "85520332-cc96-4f9e-9acf-ce583aa2954b", "0d8762ae-47d3-45ef-9d89-14e96b674221", "2305ac13-19d1-44e3-924d-4eefe68e8216" };
                                                    
        public static List<Rating> SetRatings()
        {
            var count = 6;
            List<Rating> ratings = new List<Rating>(count);
            for (int i = 1; i <= count; i++)
            {
                Rating rating = CreateRating(i);
                ratings.Add(rating);
            }
            return ratings;
        }

        public static Rating CreateRating(int id)
        {
            var rating = new Rating()
            {
                Id = id,
                ProductId = Guid.Parse(productId[id - 1]),
                CreateDate = DateTime.UtcNow           
            };
            SetReviews(rating.Id);
            return rating;
        }

        public static List<Review> SetReviews(int ratingId)
        {            
            List<Review> reviews = new List<Review>();
            var count = random.Next(1, 6);
            for (int i = 1; i <= count; i++)
            {
                var review = CreateReview(ratingId);
                reviews.Add(review);
            }
            return reviews;
        }

        public static Review CreateReview(int ratingId)
        {
            var review = new Review
            {
                Id = reviewId++,
                UserId = Guid.NewGuid().ToString(),
                Text = LoremIpsum.Substring(0, random.Next(20, 100)),
                Grade = random.Next(0, 6),
                CreateDate = DateTime.Now.AddDays(random.Next(-100, 0)),                                
                RatingId = ratingId,
                Status = ReviewStatuses.Actual
            };
            Reviews.Add(review);
            return review;
        }

        public static List<Login> SetLogins()
        {
            var logins = new List<Login>();
            var login = new Login()
            {
                Id = 1,
                UserName = "admin",
                Password = "admin"
            };
            logins.Add(login);
            return logins;
        }
    }
}