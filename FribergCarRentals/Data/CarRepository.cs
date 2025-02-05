using FribergCarRentals.Models;

namespace FribergCarRentals.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        
        public CarRepository(ApplicationDbContext applicationDbContext) 
        { 
            this.applicationDbContext = applicationDbContext;
        }

        public void Add(Car car)
        {
            applicationDbContext.Cars.Add(car); 
            applicationDbContext.SaveChanges();
        }
        public void Update(Car car)
        {
            applicationDbContext.Cars.Update(car);
            applicationDbContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            applicationDbContext.Cars.Remove(car);
            applicationDbContext.SaveChanges();
        }

        public IEnumerable<Car> GetAll()
        {
            return applicationDbContext.Cars.OrderBy(c => c.ModelYear);
        }

        public Car GetByID(int id)
        {
            return applicationDbContext.Cars.FirstOrDefault(c => c.Id == id);
        }


    } 
}
