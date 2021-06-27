using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityTransport.Data.Models
{
    [Table("Transports")]
    public class Transport : BaseModel<int>
    {
       
       // public string UserId { get; set; }
        //public User User { get; set; }
        public int TransportID { get; set; }
        public string TransportKind { get; set; }
        public string TransportType { get; set; }
        public string TransportNumber { get; set; }
        //public City City { get; set; }
        [ForeignKey("CityName")]
        public string CityName { get; set; }
    }
}
