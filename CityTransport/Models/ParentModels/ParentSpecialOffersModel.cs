using CityTransport.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.ParentModels
{
    public class ParentSpecialOffersModel
    {
        public User User { get; set; }
        public int SpecialOfferId { get; set; }
        public string Role { get; set; }

        public List<SelectListItem> RoleTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Adult", Text = "Adult"},
            new SelectListItem { Value = "Student", Text = "Student"},
            new SelectListItem { Value = "University Student", Text = "University Student"},
            new SelectListItem { Value = "Retired", Text = "Retired"},
            new SelectListItem { Value = "Parent", Text = "Parent"},
         };
        public SpecialOffers SpecialOffers { get; set; }
        public List<SpecialOffers> Offers { get; internal set; }
        // public IEnumerable<SpecialOffers> Offers { get; set; }
          public string RoleID { get; set; }
          public int SpecialOffersID { get; set; }
          public string OfferName { get; set; }
          public string OfferDescription { get; set; }
          public double OfferPrice { get; set; }
       // public bool IsChecked { get; set; }

    }
}
