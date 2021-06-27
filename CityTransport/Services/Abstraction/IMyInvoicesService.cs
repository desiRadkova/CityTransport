using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services.Abstraction
{
    public interface IMyInvoicesService
    {
        MyInvoices GetInvoicesById(string UserId);
        IEnumerable<MyInvoices> GetAllInvoices();
        void Edit(MyInvoices invoice);

        void Add(MyInvoices invoice);
    }
}
