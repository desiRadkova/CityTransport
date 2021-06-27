using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services.Abstraction
{
    public interface ISpecialOffersService
    {
        SpecialOffers GetOffersById(string offerId);
        SpecialOffers GetOffersByRole(string userRole);
        IEnumerable<SpecialOffers> GetAllOffers();
        
    }
}
