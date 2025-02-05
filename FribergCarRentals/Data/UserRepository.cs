using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Add(User user)
        {
            applicationDbContext.Users.Add(user);
            applicationDbContext.SaveChanges();
        }
        public void Update(User user)
        {
            applicationDbContext.Users.Update(user);
            applicationDbContext.SaveChanges();
        }

        public void Delete(User user)
        {
            applicationDbContext.Users.Remove(user);
            applicationDbContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return applicationDbContext.Users.OrderBy(u => u.LastName);
        }

        public User GetByID(int id)
        {
            return applicationDbContext.Users.Find(id);
        }


    }
}
