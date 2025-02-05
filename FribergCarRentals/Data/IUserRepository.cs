using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public interface IUserRepository
    {
        User GetByID(int id);
        IEnumerable<User> GetAll();
        
        void Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}
