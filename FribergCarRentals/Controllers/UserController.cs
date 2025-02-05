﻿using FribergCarRentals.Data;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentals.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View(userRepository.GetAll());
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View(userRepository.GetByID(id));
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userRepository.Add(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(userRepository.GetByID(id));    
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userRepository.Update(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(userRepository.GetByID(id));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User user)
        {
            try
            {
                userRepository.Delete(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
