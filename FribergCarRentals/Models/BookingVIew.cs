namespace FribergCarRentals.Models
{
    public class BookingView
    {
        public int Id { get; set; }
        public string Car { get; set; }
        public string User { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
