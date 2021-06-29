using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.ParentModels
{
    public class ParentChildrenInfoModel
    {
       
        public User User { get; set; }
        public List<Parent> ParenChildrenList { get; internal set; }
        public Card Card { get; set; }
        public Parent Parent { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ChildrenFirstName { get; set; }
        public string ChildrenLastName { get; set; }
        public string ChildrenId { get; set; }
        public int CardNumber { get; set; }
        public int ChildrenCardNumber { get; set; }
    }
}
