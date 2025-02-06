﻿namespace FribergCarRentals.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public int CarId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
