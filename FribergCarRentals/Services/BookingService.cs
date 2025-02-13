using FribergCarRentals.Data;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FribergCarRentals.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void ValidateAndCreate(string userId, int carId, DateTime startDate, DateTime endDate)
        {
            // Validate user existence
            if (!_bookingRepository.DoesUserExist(userId))
            {
                throw new Exception("User does not exist.");
            }

            // Validate car existence
            if (!_bookingRepository.DoesCarExist(carId))
            {
                throw new Exception("Car does not exist.");
            }

            // Validate start and end dates
            if (startDate < DateTime.Now)
            {
                throw new Exception("Start date cannot be in the past.");
            }

            if (endDate <= startDate)
            {
                throw new Exception("End date must be after the start date.");
            }

            // Check car availability
            if (!_bookingRepository.IsCarAvailable(carId, startDate, endDate))
            {
                throw new Exception("Car is not available for the selected dates.");
            }

            // Create and save the booking
            var booking = new Booking
            {
                UserId = userId,
                CarId = carId,
                Start = startDate,
                End = endDate
            };

            _bookingRepository.Add(booking);
        }

        public void DeleteBooking(Booking booking)
        {
            _bookingRepository.Delete(booking);
        }

        public IEnumerable<Car> GetCars()
        {
            return _bookingRepository.GetAllCars();
        }

        public IEnumerable<IdentityUser> GetUsers()
        {
            return _bookingRepository.GetAllUsers();
        }

        public Booking GetByID(int id)
        {
            return _bookingRepository.GetByID(id);
        }

        public BookingViewModel GetBookingView(int id)
        {
            return _bookingRepository.GetBookingView(id);
        }

        public IEnumerable<BookingViewModel> GetBookingsView()
        {
            return _bookingRepository.GetBookingsView();
        }

        public void Update(Booking booking)
        {
            _bookingRepository.Update(booking);
        }
    }
}
