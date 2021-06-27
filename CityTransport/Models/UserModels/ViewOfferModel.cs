using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.UserModels
{
    public class ViewOfferModel
    {
        public User User { get; set; }
        public int SpecialOfferId { get; set; }
        public SpecialOffers Offer { get; set; }
        public List<SpecialOffers> SpecialOffers { get; internal set; }
        public string RoleID { get; set; }
        public int SpecialOffersID { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set; }
        public double OfferPrice { get; set; }

    }
}
