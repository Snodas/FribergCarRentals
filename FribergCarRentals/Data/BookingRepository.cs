using FribergCarRentals.Models;
using FribergCarRentals.Services;
using Microsoft.AspNetCore.Identity;

namespace FribergCarRentals.Data
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BookingRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }     

        public void Add(Booking booking)
        {
            applicationDbContext.Bookings.Add(booking);
            applicationDbContext.SaveChanges();
        }
        
        public void Update(Booking booking)
        {
            applicationDbContext.Bookings.Update(booking);
            applicationDbContext.SaveChanges();
        }
        
        public void Delete(Booking booking)
        {
            applicationDbContext.Bookings.Remove(booking);
            applicationDbContext.SaveChanges();
        }

        public IEnumerable<Booking> GetAll()
        {
            return applicationDbContext.Bookings.OrderBy(b => b.Id);
        }

        public IEnumerable<Car> GetAllCars()
        {
            return applicationDbContext.Cars.ToList();
        }
        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return applicationDbContext.Users.ToList();
        }

        public IEnumerable<BookingViewModel> GetBookingsView()
        {
            return from bs in applicationDbContext.Bookings
                   join cs in applicationDbContext.Cars on bs.CarId equals cs.Id
                   join us in applicationDbContext.Users on bs.UserId equals us.Id
                   select new BookingViewModel
                   {
                       Id = bs.Id,
                       CarId = bs.CarId,
                       Car = cs.Brand + " " + cs.Model,
                       UserId = bs.UserId,
                       User = us.UserName,
                       Start = bs.Start,
                       End = bs.End
                   };
        }



        public BookingViewModel GetBookingView(int id)
        {
            return (from bs in applicationDbContext.Bookings
                   join cs in applicationDbContext.Cars on bs.CarId equals cs.Id
                   join us in applicationDbContext.Users on bs.UserId equals us.Id
                   where bs.Id == id
                   select new BookingViewModel
                   {
                       Id = bs.Id,
                       CarId = bs.CarId,
                       Car = cs.Brand + " " + cs.Model,
                       UserId = bs.UserId,
                       User = us.UserName,
                       Start = bs.Start,
                       End = bs.End
                   }).FirstOrDefault();
        }

        public bool DoesUserExist(string userId)
        {
            return applicationDbContext.Users.Any(u => u.Id == userId);
        }

        public bool DoesCarExist(int carId)
        {
            return applicationDbContext.Cars.Any(c => c.Id == carId);
        }

        public bool IsCarAvailable(int carId, DateTime startDate, DateTime endDate)
        {
            return !applicationDbContext.Bookings
                .Where(b => b.CarId == carId)
                .Any(b => b.Start < endDate && b.End > startDate);
        }

        public Booking GetByID(int id)
        {
            return applicationDbContext.Bookings.FirstOrDefault(b => b.Id == id);   
        }

        public IEnumerable<BookingViewModel> GetBookingsByUserId(string userId)
        {
            return from bs in applicationDbContext.Bookings
                   join cs in applicationDbContext.Cars on bs.CarId equals cs.Id
                   join us in applicationDbContext.Users on bs.UserId equals us.Id
                   where bs.UserId == userId
                   select new BookingViewModel
                   {
                       Id = bs.Id,
                       CarId = bs.CarId,
                       Car = cs.Brand + " " + cs.Model,
                       UserId = bs.UserId,
                       User = us.UserName,
                       Start = bs.Start,
                       End = bs.End
                   };
        }
    }
}
