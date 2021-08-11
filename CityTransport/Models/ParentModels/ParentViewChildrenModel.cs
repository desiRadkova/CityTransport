using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.ParentModels
{
    public class ParentViewChildrenModel
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public Children Children { get; set; }
        public Parent Parent { get; set; }
        public string ChildrenFirstName { get; set; }
        public string ChildrenLastName { get; set; }
        public int ChildrenCardNumber { get; set; }
        public Card Card { get; set; }
        public int CardNumber { get; set; }
        public string ChildrenId { get; set; }

    }
}
