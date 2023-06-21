using Reviews.Domain.Models;

namespace Reviews.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly DatabaseContext databaseContext;

        public LoginService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public bool CheckLogin(Login login)
        {
            var containsLogin = databaseContext.Logins;
            foreach (var item in containsLogin)
            {
                if(item.UserName.Equals(login.UserName) && item.Password.Equals(login.Password))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
