using CityTransport.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.UserModels
{
    public class ChargeCardOneKindModel
    {
        public User User { get; set; }
       
        public string City { get; set; }
       // public MyInvoices MyInvoices { get; set; }
        public Order Order { get; set; }
        public string TransportKind { get; set; }
        public string TransportType { get; set; }
        public List<SelectListItem> TransportTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Tram", Text = "Tram"},
            new SelectListItem { Value = "Bus", Text = "Bus"},
            new SelectListItem { Value = "Trolley", Text = "Trolley"},
            new SelectListItem { Value = "Subway", Text = "Subway"},
            
         };
        public string TransportNumber { get; set; }
        public List<SelectListItem> TransportNumbers { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "1"},
            new SelectListItem { Value = "2", Text = "2"},
            new SelectListItem { Value = "3", Text = "3"},
            new SelectListItem { Value = "4", Text = "4"},
            new SelectListItem { Value = "5", Text = "5"},
            new SelectListItem { Value = "6", Text = "6"},
            new SelectListItem { Value = "7", Text = "7"},
            new SelectListItem { Value = "8", Text = "8"},
            new SelectListItem { Value = "9", Text = "9"},
            new SelectListItem { Value = "10", Text = "10"},
            new SelectListItem { Value = "11", Text = "11"},
            new SelectListItem { Value = "12", Text = "12"},
            new SelectListItem { Value = "13", Text = "13"},
            new SelectListItem { Value = "14", Text = "14"},
            new SelectListItem { Value = "15", Text = "15"},
            new SelectListItem { Value = "16", Text = "16"},
            new SelectListItem { Value = "17", Text = "17"},
            new SelectListItem { Value = "18", Text = "18"},
            new SelectListItem { Value = "19", Text = "19"},
            new SelectListItem { Value = "20", Text = "20"},
            new SelectListItem { Value = "21", Text = "21"},
            new SelectListItem { Value = "22", Text = "22"},
            new SelectListItem { Value = "23", Text = "23"},
         };


    }
}
