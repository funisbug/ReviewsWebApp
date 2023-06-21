using AutoMapper;
using Reviews.Domain.Models;
using ReviewsWebApplication.Models;

namespace ReviewsWebApplication.Hellpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Review, ReviewApi>();
            CreateMap<NewReviewApi, Review>();
            CreateMap<LoginApi, Login>();
        }
    }
}
