using Reviews.Domain.Models;

namespace Reviews.Domain.Services
{
    public interface ILoginService
    {
        bool CheckLogin(Login login);
    }
}
