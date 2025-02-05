using FribergCarRentals.Data;

namespace FribergCarRentals.Services
{
    public class LoginService
    {
        private readonly ApplicationDbContext applicationDbContext; 

        public LoginService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public bool IsUserValid()
        {
            return true;
        }

        public bool IsPasswordValid()
        {
            return true;
        }

        public bool IsEmailValid()
        {
            return true;
        }

        public bool IsUserAdmin()
        {
            return true;
        }
    }
}
