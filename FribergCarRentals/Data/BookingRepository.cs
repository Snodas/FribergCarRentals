using FribergCarRentals.Models;
using FribergCarRentals.Services;

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
        public IEnumerable<User> GetAllUsers()
        {
            return applicationDbContext.Users.ToList();
        }

        public IEnumerable<BookingView> GetBookingView()
        {
            return from bs in applicationDbContext.Bookings
                   join cs in applicationDbContext.Cars on bs.CarId equals cs.Id  
                   join us in applicationDbContext.Users on bs.UserId equals us.Id
                   select new BookingView { 
                        Id = bs.Id, 
                       Car = cs.Brand + " " + cs.Model, 
                       User = us.FirstName + " " + us.LastName, 
                       Start = bs.Start, 
                       End = bs.End };   
        }

        public Booking GetByID(int id)
        {
            return applicationDbContext.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public bool DoesUserExist(int userId)
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



        //public Booking MakeBooking(int userId, int carId, DateTime startDate, DateTime endDate)
        //{
        //    // Validate user existence
        //    var user = applicationDbContext.Users.FirstOrDefault(u => u.Id == userId);
        //    if (user == null)
        //    {
        //        throw new Exception("User does not exist.");
        //    }

        //    // Validate car existence
        //    var car = applicationDbContext.Cars.FirstOrDefault(c => c.Id == carId);
        //    if (car == null)
        //    {
        //        throw new Exception("Car does not exist.");
        //    }

        //    // Validate start and end dates
        //    if (startDate < DateTime.Now)
        //    {
        //        throw new Exception("Start date cannot be in the past.");
        //    }

        //    if (endDate <= startDate)
        //    {
        //        throw new Exception("End date must be after the start date.");
        //    }

        //    // Check car availability
        //    bool isCarAvailable = !applicationDbContext.Bookings
        //        .Where(b => b.Car.Id == car.Id)
        //        .Any(b => b.Start < endDate && b.End > startDate);

        //    if (!isCarAvailable)
        //    {
        //        throw new Exception("Car is not available for the selected dates.");
        //    }

        //    // Create and save the booking
        //    var booking = new Booking
        //    {
        //        User = user,
        //        Car = car,
        //        Start = startDate,
        //        End = endDate
        //    };

        //    applicationDbContext.Bookings.Add(booking);
        //    applicationDbContext.SaveChanges();

        //    return booking;
        //}
    }
}
