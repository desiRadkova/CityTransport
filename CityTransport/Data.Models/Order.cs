using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Data.Models
{
    [Table("Order")]
    public class Order : BaseModel<int>
    {
        [ForeignKey("UserID")]
        // [Key]
        public string UserId { get; set; }
        public User User { get; set; }
        public Transport Transport { get; set; }
        [ForeignKey("TransportKind")]
        public string TransportKind { get; set; }
        public string TransportType { get; set; }
        public string TransportNumber { get; set; }
        public int Duration { get; set; }
        public Card Card { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }
        public double StandartPrice { get; set; }
    }
}
