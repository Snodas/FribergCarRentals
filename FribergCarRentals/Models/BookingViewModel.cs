using System;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Car { get; set; }
        public string UserId { get; set; }
        public string User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
    }
}
