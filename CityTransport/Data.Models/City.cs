using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityTransport.Data.Models
{
    [Table("Cities")]
    public class City : BaseModel<int>
    {
        [ForeignKey("CityID")]
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int CityPostCode { get; set; }
    }
}
