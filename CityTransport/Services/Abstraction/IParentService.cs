using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services.Abstraction
{
    public interface IParentService
    {
       
        Parent GetChildrenById(string userId);
        IEnumerable<Parent> GetAllParents();
        void Edit(Parent parent);
        void Add(Parent parent);
        void Delete(Parent parent);
    
    }
}