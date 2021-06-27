using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityTransport.Data.Models;

namespace CityTransport.Data.Repository
{
    public interface IRepository<T, TKey> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(TKey id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
      
    }
}