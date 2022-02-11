using CityTransport.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.AdminModels
{
    public class SendNotificationModel
    {
        public User User { get; set; }
        public Notifications Notifications { get; set; }
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
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
