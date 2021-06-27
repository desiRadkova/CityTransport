using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CityTransport.Data.Models
{
    [Table("Parents")]
    public class Parent : BaseModel<int>
    {
        [ForeignKey("UserID")]
        // [Key]
        public string UserId { get; set; }
        public User User { get; set; }
        public string ChildrenId { get; set; }
        public string ChildrenFirstName { get; set; }
        public string ChildrenLastName { get; set; }
        public int ChildrenCardNumber { get; set; }
       
    }
}
