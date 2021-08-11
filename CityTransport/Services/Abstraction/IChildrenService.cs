using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services.Abstraction
{
    public interface IChildrenService
    {
       
        Children GetChildrenById(string userId);
        IEnumerable<Children> GetAllParents();
        void Edit(Children children);
        void Add(Children children);
        void Delete(Children children);
    
    }
}