using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.UserModels
{
    public class UserHomePageModel
    {
        public User User { get; set; }
        public string FirstName { get; set; }

        public string Gender { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public long EGN { get; set; }
        public string Role { get; set; }
        public string City { get; set; }
        public Card Card { get; set; }
        public DateTime EndDate { get; set; }


        public DateTime NotificationThreeDay
        {
            get { return this.EndDate.AddDays(-3); }
        }
        public DateTime NotificationTwoDay
        {
            get { return this.EndDate.AddDays(-2); }
        }
        public DateTime NotificationDay
        {
            get { return this.EndDate.AddDays(-1); }
        }
    }
}
