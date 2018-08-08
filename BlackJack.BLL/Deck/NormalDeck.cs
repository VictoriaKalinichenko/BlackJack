using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.BLL.Randomize;

namespace BlackJack.BLL.Deck
{
    public class NormalDeck : IDeck
    {
        IUnitOfWork db;

        public NormalDeck(IUnitOfWork repository)
        {
            db = repository;
        }


        List<Card> cards;

        public void Create()
        {
            cards = db.Cards.SelectAll().ToList();
        }

        public Card SelectCard()
        {
            Card card;

            IRandomize randomize = new GameRandomize();
            card = cards[randomize.CardIdSelection(cards.Count)];

            return card;
        }
    }
}
