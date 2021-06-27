using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services.Abstraction
{
    public interface IOrderService
    {
        Order GetOrderById(string UserId);
        IEnumerable<Order> GetAllOrders();
        void Add(Order order);
        void Edit(Order order);
        void Delete(Order order);

    }
}
