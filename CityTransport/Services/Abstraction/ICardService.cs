using CityTransport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services
{
    public interface ICardService
    {
        Card GetCardById(int cardId);
        void AddCard(Card card);
        IEnumerable<Card> GetAllCards();
        IEnumerable<Card> GetAllByUser(string userId);
        void Edit(Card card);
        void Add(Card card);


} }
