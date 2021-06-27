using CityTransport.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.ParentModels
{
    public class ParentChangeInformationModel
    {
        public User User { get; set; }
        public Card Card { get; set; }


        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Gender { get; set; }

        public List<SelectListItem> GenderTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Male", Text = "Male"},
            new SelectListItem { Value = "Female", Text = "Female"},
            new SelectListItem { Value = "Other", Text = "Other"},
         };

        public string Role { get; set; }

        public List<SelectListItem> RoleTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Adult", Text = "Adult"},
            new SelectListItem { Value = "Student", Text = "Student"},
            new SelectListItem { Value = "University Student", Text = "University Student"},
            new SelectListItem { Value = "Retired", Text = "Retired"},
            new SelectListItem { Value = "Parent", Text = "Parent"},
         };

        public string City { get; set; }

        public List<SelectListItem> CityTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Sofia", Text = "Sofia"},
            new SelectListItem { Value = "Varna", Text = "Varna"},
            new SelectListItem { Value = "Burgas", Text = "Burgas"},
            new SelectListItem { Value = "Plovdiv", Text = "Plovdiv"},
         };
        [EmailAddress]
        public string Email { get; set; }
        // [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
        public int CardNumber { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
        public string EGN { get; set; }

    }
}
