using FribergCarRentals.Models;

namespace FribergCarRentals.Services
{
    public interface IBookingService
    {
        void ValidateAndCreate(User user, Car car, DateTime startDate, DateTime endDate);
        void DeleteBooking(Booking booking);    

        IEnumerable<Car> GetCars();
        IEnumerable<User> GetUsers();
        Booking GetByID(int Id);
        IEnumerable<BookingView> GetBookingView();
    }
}
