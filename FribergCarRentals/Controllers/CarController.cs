using FribergCarRentals.Data;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FribergCarRentals.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarController : Controller
    {
        private readonly ICarRepository carRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CarController(ICarRepository carRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.carRepository = carRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: CarController
        public ActionResult Index()
        {
            return View(carRepository.GetAll());
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            var car = carRepository.GetByID(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            return View(new CarViewModel());
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CarViewModel carViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var car = new Car
                    {
                        Brand = carViewModel.Brand,
                        Model = carViewModel.Model,
                        ModelYear = carViewModel.ModelYear
                    };

                    if (carViewModel.Picture != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await carViewModel.Picture.CopyToAsync(ms);
                            car.Image = ms.ToArray();
                            car.ImageFormat = carViewModel.Picture.ContentType;
                        }
                    }

                    carRepository.Add(car);
                    return RedirectToAction(nameof(Index));
                }
                return View(carViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(carViewModel);
            }
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            var car = carRepository.GetByID(id);
            if (car == null)
            {
                return NotFound();
            }

            var carViewModel = new CarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                ModelYear = car.ModelYear
            };

            return View(carViewModel);
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CarViewModel carViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var car = carRepository.GetByID(carViewModel.Id);
                    if (car == null)
                    {
                        return NotFound();
                    }

                    car.Brand = carViewModel.Brand;
                    car.Model = carViewModel.Model;
                    car.ModelYear = carViewModel.ModelYear;

                    if (carViewModel.Picture != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await carViewModel.Picture.CopyToAsync(ms);
                            car.Image = ms.ToArray();
                            car.ImageFormat = carViewModel.Picture.ContentType;
                        }
                    }

                    carRepository.Update(car);
                    return RedirectToAction(nameof(Index));
                }
                return View(carViewModel);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            var car = carRepository.GetByID(id);
            if (car == null)
            {
                return NotFound();
            }

            var carViewModel = new CarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                ModelYear = car.ModelYear
            };

            return View(carViewModel);
        }

        // POST: CarController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(CarViewModel carViewModel)
        {
            try
            {
                var car = carRepository.GetByID(carViewModel.Id);
                if (car != null)
                {
                    carRepository.Delete(car);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
