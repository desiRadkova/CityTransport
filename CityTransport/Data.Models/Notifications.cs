using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Data.Models
{
    [Table("Notifications")]
    public class Notifications : BaseModel<int>
    {
        public string TransportType { get; set; }
        public string TransportNumber { get; set; }
        public string Message { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }
       
    }
}
