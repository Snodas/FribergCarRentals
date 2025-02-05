using FribergCarRentals.Data;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace FribergCarRentals.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
       
        public BookingService(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public void ValidateAndCreate(User user, Car car, DateTime startDate, DateTime endDate)
        {


            if (user == null || car == null)
                throw new ArgumentNullException("User or Car cannot be null.");

            if (startDate >= endDate)
                throw new ArgumentException("Start date must be earlier than end date.");

            try
            {
                if (!bookingRepository.DoesUserExist(user.Id))
                    throw new Exception($"User with ID {user.Id} does not exist in the database.");

                if (!bookingRepository.DoesCarExist(car.Id))
                    throw new Exception($"Car with ID {car.Id} does not exist in the database.");

                if (!bookingRepository.IsCarAvailable(car.Id, startDate, endDate))
                    throw new Exception($"Car with ID {car.Id} is not available for the selected dates.");

                var booking = new Booking
                {
                    UserId = user.Id,
                    CarId = car.Id,
                    Start = startDate,
                    End = endDate
                };

                bookingRepository.Add(booking);
                Console.WriteLine("Booking created successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Booking validation or creation failed: {ex.Message}");
            }
        }

        public void DeleteBooking(Booking booking)
        {
            bookingRepository.Delete(booking);
        }

        public Booking GetByID(int id)
        {
            return bookingRepository.GetByID(id);
        }

        public IEnumerable<BookingView> GetBookingView()
        {
            return bookingRepository.GetBookingView();
        }

        public IEnumerable<Car> GetCars()
        {
            return bookingRepository.GetAllCars();
        }

        public IEnumerable<User> GetUsers()
        {
            return bookingRepository.GetAllUsers();
        }

        //public bool IsCarAvailable(Car car, DateTime startDate, DateTime endDate)
        //{
        //    return !applicationDbContext.Bookings
        //        .Where(b => b.Car.Id == car.Id)
        //        .Any(b => b.Start < endDate && b.End > startDate);
        //}

        //public bool DoesUserExist(User user)
        //{
        //    return applicationDbContext.Users.Any(u => u.Id == user.Id);
        //}

        //public bool IsStartDateValid(DateTime startDate)
        //{
        //    return startDate >= DateTime.Now;
        //}
        //public bool IsStartDateAvailable(DateTime startDate)
        //{
        //    return !applicationDbContext.Bookings.Any(b => b.Start == startDate);
        //}

        //public bool IsEndDateValid(DateTime startDate, DateTime endDate)
        //{
        //    return endDate > startDate;
        //}
        //public bool IsEndDateAvailable(DateTime endDate)
        //{
        //    return !applicationDbContext.Bookings.Any(b => b.End == endDate);
        //}

    }
}
