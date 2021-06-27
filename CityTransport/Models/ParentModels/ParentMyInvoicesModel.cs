using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.ParentModels
{
    public class ParentMyInvoicesModel
    {
        public User User { get; set; }
        public string UserId { get; set; }
        // public MyInvoices MyInvoices { get; set; }
        public List<MyInvoices> MyInvoices { get; internal set; }
        //public int 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SpecialOffers SpecialOffers { get; set; }
        public string SpecialOfferName { get; set; }
        public Card Card { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
