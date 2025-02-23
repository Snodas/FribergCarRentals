using FribergCarRentals.Models;
using Microsoft.AspNetCore.Identity;

namespace FribergCarRentals.Services
{
    public interface IBookingService
    {
        void ValidateAndCreate(string userId, int carId, DateTime startDate, DateTime endDate);
        void DeleteBooking(Booking booking);
        void Update(Booking booking);
        
        IEnumerable<Car> GetCars();
        IEnumerable<IdentityUser> GetUsers();
       
        BookingViewModel GetBookingView(int Id);
        IEnumerable<BookingViewModel> GetBookingsView();
        Booking GetByID(int Id);
        IEnumerable<BookingViewModel> GetBookingsByUserId(string userId);
    }
}
