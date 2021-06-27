using CityTransport.Data.Models;
using CityTransport.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services
{
    public class CityService : ICityService
    {
       /* private readonly IRepository<User, string> usersRepo;
        private readonly IRepository<City, string> cityRepo;
        private readonly IRepository<Transport, int> transportRepo;
        private readonly IRepository<Card, int> cardRepo;

        public CityService(
            IRepository<User, string> usersRepo,
            IRepository<City, string> cityRepo,
            IRepository<Transport, int> transportRepo,
            IRepository<Card, int> cardRepo)
        {
            this.usersRepo = usersRepo;
            this.cityRepo = cityRepo;
            this.transportRepo = transportRepo;
            this.cardRepo = cardRepo;

        }
        public IEnumerable<City> GetAllComments()
        {
            return cityRepo.GetAll();
        }
        public IEnumerable<City> GetCityOfUser(string userId)
        {
            var city = this.cityRepo
                .GetAll()
                .Where(x => x.UserId == userId).ToList();
            var commentsForDentist = new List<Comment>();
            foreach (var e in events)
            {
                commentsForDentist.AddRange(commentsRepo
                .GetAll()
               .Where(c => c.EventId == e.Id).ToList());
            }
            return commentsForDentist;
        }*/
    }
}
