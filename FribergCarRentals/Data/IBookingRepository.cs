using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public interface IBookingRepository
    {
        Booking GetByID(int id);
        IEnumerable<Booking> GetAll();
        IEnumerable<BookingView> GetBookingView();
        IEnumerable<Car> GetAllCars();
        IEnumerable<User> GetAllUsers();

        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);

        bool DoesUserExist(int userId);
        bool DoesCarExist(int carId);
        bool IsCarAvailable(int carId, DateTime startDate, DateTime endDate);   

    }
}
