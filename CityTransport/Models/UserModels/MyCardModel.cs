using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.UserModels
{
    public class MyCardModel
    {
        public User User { get; set; }
        public Card Card { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
        public int CardNumber { get; set; }
    }

    
}
