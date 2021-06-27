using CityTransport.Data;
using CityTransport.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Repositories
{
    public class UsersRepository<T, TKey>
     where T : CityTransport.Data.Models.User
    {
        public UsersRepository(CityTransportDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            this.Context = context;
           
        }

        protected DbSet<T> DbSet { get; set; }

        protected CityTransportDbContext Context { get; set; }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await this.DbSet.FindAsync(id);
        }

        public T GetById(TKey id)
        {
            return this.DbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return this.DbSet;
        }
    }
}