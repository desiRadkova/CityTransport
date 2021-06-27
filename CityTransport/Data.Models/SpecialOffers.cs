using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityTransport.Data.Models
{
    [Table("SpecialOffers")]
    public class SpecialOffers : BaseModel<int>
    {
        [ForeignKey("UserID")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("RoleID")]
        public string RoleID { get; set; }
        public int SpecialOffersID { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set; }
        public double OfferPrice { get; set; }
    }
}
