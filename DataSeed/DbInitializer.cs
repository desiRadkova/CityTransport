using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CityTransport.Common;
using CityTransport.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dbmodel = CityTransport.Data.Models;

namespace DataSeed
{
    public class DbInitializer
    {
        private IServiceProvider serviceProvider;
        private CityTransportDbContext context;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            context = this.serviceProvider.GetRequiredService<CityTransportDbContext>();
        }

       
            public void Initialize()
            {
                context.Database.Migrate();
                AddRoles();
                SeedUsers();
                // SeedParents();
                SeedCards();
                SeedCities();
                SeedSpecialOffers();
                SeedTransports();
                SeedMyInvoices();
            }

            private void AddRoles()
            {
                
                Console.WriteLine("Adding Roles...");
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roleCheck = roleManager.RoleExistsAsync(GlobalConstants.ParentRole).Result;
         
                //-----------Adult
                roleCheck = roleManager.RoleExistsAsync(GlobalConstants.UserAdultRole).Result;
                if (!roleCheck)
                {
                    roleManager.CreateAsync(new IdentityRole(GlobalConstants.UserAdultRole)).Wait();
                }
           
            }

            private void SeedUsers()
            {
            Console.WriteLine("Adding Users...");
            var userManager = this.serviceProvider.GetRequiredService<UserManager<Dbmodel.User>>();


            // The security stamp is used to invalidate a users login cookie and force them to re-login.
            var users = new List<Dbmodel.User>()
            {
                new Dbmodel.User
                {
                    UserName = "peter@gmail.com",
                    Email = "peter@gmail.com",
                    FirstName = "Peter",
                    LastName = "Petrov",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Gender = "Male",
                    EGN = 7207130000,
                    Role = "Retired",
                    SpecialOfferId = 112
                  
                },
                new Dbmodel.User
                {
                    UserName = "annie.dimova@gmail.com",
                    Email = "annie.dimova@gmail.com",
                    FirstName = "Annie",
                    LastName = "Dimova",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Gender = "Female",
                    EGN = 8545632587,
                    Role = "Parent",
                    SpecialOfferId = 111
                },
                 new Dbmodel.User
                {
                    UserName = "maria.georgieva@gmail.com",
                    Email = "maria.georgieva@gmail.com",
                    FirstName = "Maria",
                    LastName = "Georgieva",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Gender = "Female",
                    EGN = 5423985123,
                    City = "Sofia",
                    Role = "Adult",
                    SpecialOfferId = 116
                },
                  new Dbmodel.User
                {
                    UserName = "martin.dimov@gmail.com",
                    Email = "maertin.dimov@gmail.com",
                    FirstName = "Martin",
                    LastName = "Dimov",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Gender = "Male",
                    EGN = 0504215465,
                    City = "Varna",
                    Role = "Student",
                    SpecialOfferId = 112
                },
                   new Dbmodel.User
                {
                    UserName = "ivan.ivanov@gmail.com",
                    Email = "ivan.ivanov@gmail.com",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Gender = "Male",
                    EGN = 0104215465,
                    City = "Sofia",
                    Role = "Student",
                    SpecialOfferId = 113
                },
                    new Dbmodel.User
                {
                    UserName = "jenifer.haris@gmail.com",
                    Email = "jenifer.haris@gmail.com",
                    FirstName = "Jenifer",
                    LastName = "Haris",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Gender = "Female",
                    EGN = 9504215465,
                    City = "Sofia",
                    Role = "University Student",
                    SpecialOfferId = 115
                },
                     new Dbmodel.User
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Gender = "",
                    EGN = 0,
                    City = "Sofia",
                    Role = "Admin",
                    SpecialOfferId = 112
                }

            };

            foreach (var user in users)
            {
                userManager.CreateAsync(user, "qwerty123@").Wait();
                userManager.AddToRoleAsync(user, GlobalConstants.UserAdultRole).Wait();
            }
        }

       /* private void SeedParents()
         {
            Console.WriteLine("Adding Parents...");
            List<Dbmodel.User> users = context.Users.Where(u => u.Role == "Parent").ToList();
            List<Dbmodel.Parent> parents = new List<Dbmodel.Parent>()
            {

            };
            context.Parents.AddRange(parents);
            context.SaveChanges();
        }*/

        private void SeedCards()
        {

            Console.WriteLine("Adding Cards...");
            List<Dbmodel.User> users = context.Users.Where
               (u => u.Id != null).ToList();
            List<Dbmodel.Card> cards = new List<Dbmodel.Card>()
            {
           new Dbmodel.Card
            {
                User = users.FirstOrDefault(u => u.Email == "peter@gmail.com"),
                CardNumber = 01169759,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                StandartPrice = 20.00

            },
            new Dbmodel.Card
            {
                 User = users.FirstOrDefault(u => u.Email == "annie.dimova@gmail.com"),
                CardNumber = 02269759,
                StartDate = new DateTime(2021, 04, 20),
                EndDate = new DateTime(2021, 05, 20),
                StandartPrice = 50.00

            } };
            context.Card.AddRange(cards);
            context.SaveChanges();

        }

        //-----------------------------------


        private void SeedCities()
        {
            Console.WriteLine("Adding Cities...");
            List<Dbmodel.City> cities = new List<Dbmodel.City>() {
            new Dbmodel.City
            {
                CityID = 1,
                CityName = "Sofia",
                CityPostCode = 1000
            },
            new Dbmodel.City
            {
                CityID = 2,
                CityName = "Varna",
                CityPostCode = 9000
            },
            new Dbmodel.City
            {
                CityID = 3,
                CityName = "Plovdiv",
                CityPostCode = 8000
            },
            new Dbmodel.City
            {
                CityID = 4,
                CityName = "Burgas",
                CityPostCode = 2000
            }};
            context.City.AddRange(cities);
            context.SaveChanges(); 
        }

        private void SeedSpecialOffers()
        {
            Console.WriteLine("Adding SpecialOffers...");
            List<Dbmodel.SpecialOffers> offers = new List<Dbmodel.SpecialOffers>() { 
            new Dbmodel.SpecialOffers
            {
                RoleID = "Student",
               SpecialOffersID = 111,
               OfferName = "Student Standart",
               OfferDescription = "Studding student can have 20% discount.",
               OfferPrice = 20.00
            },
            new Dbmodel.SpecialOffers
            {
                RoleID = "Adult",
                SpecialOffersID = 112,
                OfferName = "Adult One Year",
                OfferDescription = "One Year 50% discount.",
                OfferPrice = 50.00
            },
             new Dbmodel.SpecialOffers
            {
                RoleID = "Adult",
                SpecialOffersID = 116,
                OfferName = "Adult Green Ticket",
                OfferDescription = "Too durty air in the city. Thats why you could use" +
                "city transport.",
                OfferPrice = 01.00
            },
             new Dbmodel.SpecialOffers
            {
                RoleID = "Adult",
                SpecialOffersID = 115,
                OfferName = "One Day All Kind",
                OfferDescription = "This is a day card for All Kind of Transprts.",
                OfferPrice = 01.00
            },
            new Dbmodel.SpecialOffers
            {
                RoleID = "Parent",
                SpecialOffersID = 113,
                OfferName = "Parent Children",
                OfferDescription = "Parent One Children Discount.",
                OfferPrice = 60.00
            },
             new Dbmodel.SpecialOffers
            {
                RoleID = "Parent",
                SpecialOffersID = 114,
                OfferName = "Parent Two Children",
                OfferDescription = "Parent + Two Children Discount.",
                OfferPrice = 60.00
            } };
            context.SpecialOffers.AddRange(offers);
            context.SaveChanges();
        }
        private void SeedTransports()
        {
            Console.WriteLine("Adding Transports...");
            List<Dbmodel.Transport> transports = new List<Dbmodel.Transport>() {
            new Dbmodel.Transport
            {

                TransportID = 255,
                TransportKind = "All Kind",
                TransportNumber = "All Kind",
                TransportType = "",
                CityName = "Sofia"

           },
             new Dbmodel.Transport
            {

                TransportID = 000,
                TransportKind = "One Kind",
                TransportNumber = "One Kind",
                TransportType = "",
                CityName = "Sofia"

           }};

            context.Transport.AddRange(transports);
            context.SaveChanges();
        }
        //Have to drop the db and create it again
        private void SeedMyInvoices()
        {
            Console.WriteLine("Adding Invoices...");
            List<Dbmodel.User> users = context.Users.Where
              (u => u.Id != null).ToList();
            List<Dbmodel.MyInvoices> myInvoices = new List<Dbmodel.MyInvoices>() {
            new Dbmodel.MyInvoices
            {
                 User = users.FirstOrDefault(u => u.Email == "peter@gmail.com"),
               TransportKind = "All Kind",
               StandartPrice = 50,
               StartDate = DateTime.Now,
               EndDate = DateTime.Now.AddMonths(1)
            },
              new Dbmodel.MyInvoices
            {
                 User = users.FirstOrDefault(u => u.Email == "peter@gmail.com"),
               TransportKind = "All Kind",
               StandartPrice = 100,
               StartDate = new DateTime(2021,02,23),
               EndDate = new DateTime(2021,02,23).AddMonths(2)
            },
                 new Dbmodel.MyInvoices
            {
                 User = users.FirstOrDefault(u => u.Email == "peter@gmail.com"),
               TransportKind = "One Kind",
               TransportNumber = "Tram Number: 11",
               StandartPrice = 30,
               StartDate = new DateTime(2021,04,23),
               EndDate = new DateTime(2021,04,23).AddMonths(1)
            }
            };
            context.MyInvoices.AddRange(myInvoices);
            context.SaveChanges();
        }
       

    }
}