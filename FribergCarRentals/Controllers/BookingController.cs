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
        public ActionResult Index()
        {
            return View(_bookingService.GetBookingsView());
        }

        // GET: BookingController/Details/5
        public ActionResult Details(int id)
        {
            return View(_bookingService.GetBookingView(id));
        }

        // GET: BookingController/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
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

            return View();
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Booking booking)
        {
            //var user = new IdentityUser { Id = booking.UserId };
            //var car = new Car { Id = booking.CarId };

            try
            {
                if (ModelState.IsValid)
                {
                    _bookingService.ValidateAndCreate(booking.UserId, booking.CarId, booking.Start, booking.End);

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
