using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CityTransport.Data.Models
{
    [Table("Users")]
    public class User : IdentityUser
    {
       // [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long EGN { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public City CityName { get; set; }    
        public string Role { get; set; }      
        public Card Card { get; set; }
        public int SpecialOfferId { get; set; }

    }
}
