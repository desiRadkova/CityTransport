using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services.Abstraction
{
    public interface INotificationsService
    {
        
        IEnumerable<Notifications> GetAllNotifications();
       
        void Edit(Notifications notification);

        void Add(Notifications notification);
        void Delete(Notifications notification);
    }
}
