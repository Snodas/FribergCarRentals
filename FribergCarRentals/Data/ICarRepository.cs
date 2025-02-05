using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public interface ICarRepository
    {
        Car GetByID(int id);
        IEnumerable<Car> GetAll();

        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
