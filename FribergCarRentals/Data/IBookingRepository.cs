using FribergCarRentals.Models;
using Microsoft.AspNetCore.Identity;

namespace FribergCarRentals.Data
{
    public interface IBookingRepository
    {
        Booking GetByID(int id);
        IEnumerable<Booking> GetAll();
        IEnumerable<BookingViewModel> GetBookingsView();
        BookingViewModel GetBookingView(int id);
        IEnumerable<Car> GetAllCars();
        IEnumerable<IdentityUser> GetAllUsers();
        IEnumerable<BookingViewModel> GetBookingsByUserId(string userId); // Add this method

        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);

        bool DoesUserExist(string userId);
        bool DoesCarExist(int carId);
        bool IsCarAvailable(int carId, DateTime startDate, DateTime endDate);   

    }
}
