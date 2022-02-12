using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.UserModels
{
    public class PaymentMethodPageModel
    {
        public User User { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and must be at max {1} characters long.")]
        public string LastName { get; set; }
        public Order Order { get; set; }

        public int? SpecialOfferId { get; set; }
        public MyInvoices MyInvoices { get; set; }
        public string UserId { get; set; }
        public string TransportType { get; set; }
        public string TransportNumber { get; set; }
        public string TransportKind { get; set; }
        public double OfferPrice { get; set; }
        public double StandartPrice { get; set; }

        public int Duration { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate
        {
            get { return this.StartDate.AddMonths(Duration); }

        }
        [Required]
        [Range(1000000000000000, 9999999999999999, ErrorMessage = "Card Number should have at least 16 digits!")]
        public Int64 CardNumber{ get; set; }
        [Required]
        public DataType ExpireDate { get; set; }
        [Required]
        [Range(100, 999, ErrorMessage = "CV Code should have at least 3 digits!")]
        public int CVcode { get; set; }

        
        public double TotalOfferPrice
        {
            get { return this.OfferPrice * Duration; }
        }
      
        public double TotalPrice
        {
            get{ return this.StandartPrice * Duration; }
        }
    }
}
