namespace DataSeed
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using CityTransport.Data;
    using Microsoft.AspNetCore.Identity;

    class StartUp
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection();

            services.AddDbContext<CityTransportDbContext>(options =>
              options.UseSqlServer(GetConnectionString()));
            services.AddIdentity<CityTransport.Data.Models.User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<CityTransportDbContext>()
            .AddDefaultTokenProviders();

            var serviceProvider = services.BuildServiceProvider();

            InitializeData(serviceProvider);
        }

        private static void InitializeData(ServiceProvider serviceProvider)
        {
            DbInitializer initializer = new DbInitializer(serviceProvider);
            initializer.Initialize();
        }

        public static string GetConnectionString()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.
                SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false);
            var configuration = configBuilder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}