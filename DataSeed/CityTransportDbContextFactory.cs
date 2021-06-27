using CityTransport.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSeed
{
    class CityTransportDbContextFactory : IDesignTimeDbContextFactory<CityTransportDbContext>
    {
        public CityTransportDbContext CreateDbContext()
        {
            return this.CreateDbContext(null);
        }

        public CityTransportDbContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", optional: false);

            var configuration = configBuilder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<CityTransportDbContext>();
            builder.UseSqlServer(connectionString);

            return new CityTransportDbContext(builder.Options);
        }
    }
}