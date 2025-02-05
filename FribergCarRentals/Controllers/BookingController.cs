using FribergCarRentals.Data;
using FribergCarRentals.Models;
using FribergCarRentals.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FribergCarRentals.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService; 
        
        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // GET: BookingController
        public ActionResult Index()
        {
            return View(bookingService.GetBookingView());
        }

        // GET: BookingController/Details/5
        public ActionResult Details(int id)
        {
            return View(bookingService.GetByID(id));
        }

        // GET: BookingController/Create
        public IActionResult Create()
        {
            var users = bookingService.GetUsers()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.FirstName + " " + u.LastName
                }).ToList();

            ViewBag.UserId = users;

            var cars = bookingService.GetCars()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Brand + " " + c.Model 
                }).ToList();

            ViewBag.CarId = cars;

            return View();
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            var user = new User { Id = booking.UserId };
            var car = new Car { Id = booking.CarId };

            bookingService.ValidateAndCreate(user, car, booking.Start, booking.End);

            try
            {
                if (ModelState.IsValid)
                {
                    // Additional logic if needed
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(bookingService.GetByID(id));
        }

        // POST: BookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //bookingRepository.Update(booking);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(bookingService.GetByID(id));
        }

        // POST: BookingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Booking booking)
        {
            try
            {
                bookingService.DeleteBooking(booking);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
