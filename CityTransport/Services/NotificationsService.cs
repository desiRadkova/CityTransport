using CityTransport.Data.Models;
using CityTransport.Data.Repository;
using CityTransport.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly IRepository<User, string> usersRepo;
        private readonly IRepository<Notifications, string> notificationsRepo;
        private readonly IRepository<MyInvoices, string> myInvoicesRepo;

        public NotificationsService(IRepository<User, string> usersRepo,
            IRepository<Notifications, string> notificationsRepo,
            IRepository<MyInvoices, string> myInvoicesRepo)
        {
            this.usersRepo = usersRepo;
            this.notificationsRepo = notificationsRepo;
            this.myInvoicesRepo = myInvoicesRepo;
        }


        public IEnumerable<Notifications> GetAllNotifications()
        {
            return notificationsRepo.GetAll();
        }
      
        public void Edit(Notifications notification)
        {
            notificationsRepo.Update(notification);
            notificationsRepo.Save();
        }
        public void Add(Notifications notification)
        {
            notificationsRepo.Add(notification);
            notificationsRepo.Save();
        }
        public void Delete(Notifications notification)
        {

            notificationsRepo.Delete(notification);
            notificationsRepo.Save();
        }
    }
}
