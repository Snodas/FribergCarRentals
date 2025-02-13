using FribergCarRentals.Data;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FribergCarRentals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarRepository _carRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ICarRepository carRepository, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _carRepository = carRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cars = _carRepository.GetAll();
            return View(cars);
        }

        [Authorize]
        public IActionResult Book(int carId)
        {
            var car = _carRepository.GetByID(carId);
            if (car == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            return RedirectToAction("CreateForUser", "Booking", new { carId = car.Id, userId = userId });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
