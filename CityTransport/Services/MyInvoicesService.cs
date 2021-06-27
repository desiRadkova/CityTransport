using CityTransport.Data.Models;
using CityTransport.Data.Repository;
using CityTransport.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services
{
    public class MyInvoicesService : IMyInvoicesService
    {
        private readonly IRepository<User, string> usersRepo;
        private readonly IRepository<MyInvoices, string> myInvoicesRepo;

        public MyInvoicesService(IRepository<User, string> usersRepo,
            IRepository<MyInvoices, string> myInvoicesRepo)
        {
            this.usersRepo = usersRepo;
            this.myInvoicesRepo = myInvoicesRepo;
        }

        public MyInvoices GetInvoicesById(string UserId)
        {
            return myInvoicesRepo.GetById(UserId);
        }
 

        public IEnumerable<MyInvoices> GetAllInvoices()
        {
            return myInvoicesRepo.GetAll();
        }
        public void Edit(MyInvoices invoice)
        {
            myInvoicesRepo.Update(invoice);
            myInvoicesRepo.Save();
        }
        public void Add(MyInvoices invoice)
        {
            myInvoicesRepo.Add(invoice);
            myInvoicesRepo.Save();
        }
    }
}
