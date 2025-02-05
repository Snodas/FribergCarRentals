using FribergCarRentals.Models;

namespace FribergCarRentals.ViewModels
{
    public class CarViewModel
    {
        public Car car { get; set; }
        public User user { get; set; }
        public Booking booking { get; set; }
    }
}
