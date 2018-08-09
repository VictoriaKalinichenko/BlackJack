using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Cards;
using BlackJack.DAL.Interfaces;
using BlackJack.BLL.Deck;

namespace BlackJack.BLL.Players
{
    public class BjDealer : IDealer
    {
        Player dealer;
        IUnitOfWork db;
        IDeck deck;

        DealersCards dealersCards;


        public BjDealer(IUnitOfWork context, Player _dealer, IDeck _deck)
        {
            db = context;
            dealer = _dealer;
            deck = _deck;
        }


        public DealersCards Cards
        {
            get
            {
                if (dealersCards == null)
                {
                    dealersCards = new DealersCards(db, dealer, deck);
                }
                return dealersCards;
            }
        }


        public bool IsScoreNull()
        {
            bool result = false;

            if (dealer.Score <= 0)
            {
                result = true;
            }

            return result;
        }

        public Player GetDealer()
        {
            return dealer;
        }
    }
}

