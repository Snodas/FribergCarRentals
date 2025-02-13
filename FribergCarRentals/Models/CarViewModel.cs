

namespace FribergCarRentals.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ModelYear { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
