using System.ComponentModel.DataAnnotations;

namespace ReviewsWebApplication.Models
{
    public class LoginApi
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
