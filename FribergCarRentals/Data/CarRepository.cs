using FribergCarRentals.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FribergCarRentals.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CarRepository(ApplicationDbContext context)
        {
            applicationDbContext = context;
        }

        public IEnumerable<Car> GetAll()
        {
            return applicationDbContext.Cars.ToList();
        }

        public Car GetByID(int id)
        {
            return applicationDbContext.Cars.Find(id);
        }

        public void Add(Car car)
        {
            applicationDbContext.Cars.Add(car);
            applicationDbContext.SaveChanges();
        }

        public void Update(Car car)
        {
            applicationDbContext.Entry(car).State = EntityState.Modified;
            applicationDbContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            applicationDbContext.Cars.Remove(car);
            applicationDbContext.SaveChanges();
        }
    }
}
