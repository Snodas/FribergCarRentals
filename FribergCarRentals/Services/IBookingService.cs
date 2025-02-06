using FribergCarRentals.Models;
using Microsoft.AspNetCore.Identity;

namespace FribergCarRentals.Services
{
    public interface IBookingService
    {
        void ValidateAndCreate(IdentityUser user, Car car, DateTime startDate, DateTime endDate);
        void DeleteBooking(Booking booking);    

        IEnumerable<Car> GetCars();
        IEnumerable<IdentityUser> GetUsers();
        Booking GetByID(int Id);
        IEnumerable<BookingView> GetBookingView();
    }
}
