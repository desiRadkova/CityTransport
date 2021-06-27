using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services.Abstraction
{
    public interface IUsersService
    {
        IEnumerable<User> GetAllUsers();
        void Edit(User user);
        User GetUserById(string userId);
        void Delete(User user);
    }
}