using FribergCarRentals.Data;
using FribergCarRentals.Models;
using FribergCarRentals.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace FribergCarRentals.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICarRepository _carRepository;

        public BookingController(IBookingService bookingService, UserManager<IdentityUser> userManager, ICarRepository carRepository)
        {
            _bookingService = bookingService;
            _userManager = userManager;
            _carRepository = carRepository;
        }

        // GET: BookingController
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var bookings = _bookingService.GetBookingsView();

            var sortedBookings = bookings
                .OrderBy(b => b.Start > DateTime.Now)
                .ThenBy(b => b.Start <= DateTime.Now && b.End >= DateTime.Now) 
                .ThenBy(b => b.End < DateTime.Now) 
                .ToList();

            return View(sortedBookings);
        }


        // GET: BookingController/MyBookings
        [Authorize]
        public IActionResult MyBookings()
        {
            var userId = _userManager.GetUserId(User);
            var bookings = _bookingService.GetBookingsByUserId(userId);

            var sortedBookings = bookings
                .OrderBy(b => b.Start > DateTime.Now)
                .ThenBy(b => b.Start <= DateTime.Now && b.End >= DateTime.Now)
                .ThenBy(b => b.End < DateTime.Now) 
                .ToList();

            return View(sortedBookings);
        }

        // POST: BookingController/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Cancel(int id)
        {
            var booking = _bookingService.GetByID(id);
            if (booking == null || booking.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            _bookingService.DeleteBooking(booking);
            TempData["SuccessMessage"] = "Booking canceled successfully.";
            return RedirectToAction(nameof(MyBookings));
        }




        // GET: BookingController/Details/5
        public ActionResult Details(int id)
        {
            return View(_bookingService.GetBookingView(id));
        }

        // GET: BookingController/Create
        [Authorize]
        public IActionResult Create(int carId)
        {
            var users = _bookingService.GetUsers()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName,
                    Selected = u.Id == _userManager.GetUserId(User) 
                }).ToList();

            ViewBag.UserId = users;

            var cars = _bookingService.GetCars()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Brand + " " + c.Model,
                    Selected = c.Id == carId   
                }).ToList();

            ViewBag.CarId = cars;

            return View();
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bookingService.ValidateAndCreate(booking.UserId, booking.CarId, booking.Start, booking.End);
                    TempData["SuccessMessage"] = "Booking Successfull!";                  
                    
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction(nameof(MyBookings));
                    }
                }
                return View(booking);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Booking failed, please try again. " + ex.Message;

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(MyBookings));
                }
            }
        }

        // GET: BookingController/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var booking = _bookingService.GetByID(id);
            if (booking == null)
            {
                return NotFound();
            }

            var users = _bookingService.GetUsers()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                }).ToList();

            ViewBag.UserId = users;

            var cars = _bookingService.GetCars()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Brand + " " + c.Model
                }).ToList();

            ViewBag.CarId = cars;

            return View(booking);
        }

        // POST: BookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _bookingService.Update(booking);
                    return RedirectToAction(nameof(Index));
                }
                return View(booking);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var users = _bookingService.GetUsers()
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.UserName
                    }).ToList();

                ViewBag.UserId = users;

                var cars = _bookingService.GetCars()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Brand + " " + c.Model
                    }).ToList();

                ViewBag.CarId = cars;
                return View(booking);
            }
        }

        // GET: BookingController/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var booking = _bookingService.GetBookingView(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: BookingController/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _bookingService.GetByID(id);
            if (booking == null)
            {
                return NotFound();
            }

            _bookingService.DeleteBooking(booking);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
