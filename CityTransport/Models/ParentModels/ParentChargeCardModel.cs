using CityTransport.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.ParentModels
{
    public class ParentChargeCardModel
    {
        public User User { get; set; }
        public string TransportKind { get; set; }
        public List<SelectListItem> KindTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "All Kind", Text = "All Kind"},
            new SelectListItem { Value = "One Kind", Text = "One Kind"},
           
         }; 

        public string CardDuration { get; set; }

        public List<SelectListItem> DurationTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "One Month", Text = "One Month"},
            new SelectListItem { Value = "Three Months", Text = "Three Moths"},
            new SelectListItem { Value = "One Year", Text = "One Year"},
         };

        public MyInvoices MyInvoices { get; set; }
    }
}
