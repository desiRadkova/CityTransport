using CityTransport.Data.Models;
using CityTransport.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services
{
    public class CardService : ICardService
    {
        private readonly IRepository<User, string> usersRepo;
        private readonly IRepository<Card, int> cardRepo;

        public CardService(IRepository<User, string> usersRepo,
            IRepository<Card, int> cardRepo)
        {
            this.usersRepo = usersRepo;
            this.cardRepo = cardRepo;
        }
        public Card GetCardById(int cardId)
        {
            return cardRepo.GetById(cardId);
        }
        public void AddCard(Card card)
        {
            cardRepo.Add(card);
            cardRepo.Save();
        }
        public IEnumerable<Card> GetAllCards()
        {
            return cardRepo.GetAll();
        }
        public IEnumerable<Card> GetAllByUser(string userId)
        {
            throw new NotImplementedException();
        }
        public void Edit(Card card)
        {
            cardRepo.Update(card);
            cardRepo.Save();
        }
        public void Add(Card card)
        {
            cardRepo.Add(card);
            cardRepo.Save();
        }


    }
}
