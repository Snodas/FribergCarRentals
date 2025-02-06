using FribergCarRentals.Models;
using Microsoft.AspNetCore.Identity;

namespace FribergCarRentals.ViewModels
{
    public class CarViewModel
    {
        public Car car { get; set; }
        public IdentityUser user { get; set; }
        public Booking booking { get; set; }
    }
}
