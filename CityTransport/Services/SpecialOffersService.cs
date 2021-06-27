using CityTransport.Data.Models;
using CityTransport.Data.Repository;
using CityTransport.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services
{
    public class SpecialOffersService : ISpecialOffersService
    {
        private readonly IRepository<User, string> usersRepo;
        private readonly IRepository<SpecialOffers, string> specialOffersRepo;

        public SpecialOffersService(IRepository<User, string> usersRepo,
            IRepository<SpecialOffers, string> specialOffersRepo)
        {
            this.usersRepo = usersRepo;
            this.specialOffersRepo = specialOffersRepo;
        }

        public SpecialOffers GetOffersById(string offerId)
        {
            return specialOffersRepo.GetById(offerId);
        }
        public SpecialOffers GetOffersByRole(string userRole)
        {

            return specialOffersRepo.GetById(userRole);
        }
        
        public IEnumerable<SpecialOffers> GetAllOffers()
        {
            return specialOffersRepo.GetAll();
        }
    }
}
