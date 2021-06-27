using CityTransport.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.UserModels
{
    public class ViewOrderModel
    {
        public User User { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public int? SpecialOfferId { get; set; }
        public int SpecialOfferID { get; set; }
        public SpecialOffers SpecialOffers { get; set; }
        public string OfferName { get; set; }
        public double OfferPrice { get; set; }
       
        public string City { get; set; }
       // public MyInvoices MyInvoices { get; set; }
        public Order Order { get; set; }
        public string TransportKind { get; set; }
        public string TransportType { get; set; }
        public double StandartPrice { get; set; }

        //[BindProperty]
        public int Duration { get; set; }
        public List<SelectListItem> CardDuration { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "One Month"},
            new SelectListItem { Value = "2", Text = "Two Months"},
            new SelectListItem { Value = "3", Text = "Three Months"},
            new SelectListItem { Value = "12", Text = "One Year"},
        };
        public string TransportNumber { get; set; }
       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
