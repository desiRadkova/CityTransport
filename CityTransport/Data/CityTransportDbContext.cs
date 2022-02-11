using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CityTransport.Data.Models;

namespace CityTransport.Data
{
    public class CityTransportDbContext : IdentityDbContext
    {
        public CityTransportDbContext(DbContextOptions<CityTransportDbContext> options)
            : base(options)
        {
        }

        public new DbSet<User> Users { get; set; }
        public new DbSet<Parent> Parents { get; set; }
        public new DbSet<Card> Card { get; set; }
        public new DbSet<City> City { get; set; }
        public new DbSet<SpecialOffers> SpecialOffers { get; set; }
        public new DbSet<Transport> Transport { get; set; }
        public new DbSet<MyInvoices> MyInvoices { get; set; }
        public new DbSet<Order> Orders { get; set; }
        public new DbSet<Children> Childrens { get; set; }
        public new DbSet<Notifications> Notification { get; set; }


    }
}
