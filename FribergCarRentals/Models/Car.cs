﻿using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ModelYear { get; set; }
        public byte[] Image { get; set; }
        public string ImageFormat { get; set; }
    }

}
