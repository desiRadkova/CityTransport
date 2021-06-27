using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.UserModels
{
    public class ViewInvoiceModel
    {
        public User User { get; set; }
        public string UserId { get; set; }
         public MyInvoices Invoices { get; set; }
        public List<MyInvoices> MyInvoices { get; internal set; }
        public int SpecialOffersID { get; set; }
        public string OfferName { get; set; }
        public double OfferPrice { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TransportKind { get; set; }
        public string TransportType { get; set; }
        public string TransportNumber { get; set; }
        public double StandartPrice { get; set; }
        public SpecialOffers SpecialOffers { get; set; }
        public Card Card { get; set; }
        //[DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
