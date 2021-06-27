using CityTransport.Data.Models;
using CityTransport.Data.Repository;
using CityTransport.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<User, string> usersRepo;
        private readonly IRepository<Order, string> orderRepo;

        public OrderService(IRepository<User, string> usersRepo,
            IRepository<Order, string> orderRepo)
        {
            this.usersRepo = usersRepo;
            this.orderRepo = orderRepo;
        }

        public Order GetOrderById(string UserId)
        {
            return orderRepo.GetById(UserId);
        }
 

        public IEnumerable<Order> GetAllOrders()
        {
            return orderRepo.GetAll();
        }
       
        public void Add(Order order)
        {
            orderRepo.Add(order);
            orderRepo.Save();
        }
        public void Edit(Order order)
        {
            orderRepo.Update(order);
            orderRepo.Save();
        }
        public void Delete(Order order)
        {
            
            orderRepo.Delete(order);
            orderRepo.Save();
        }
    }
}
