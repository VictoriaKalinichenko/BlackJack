using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;

namespace BlackJack.BLL.CardFunctions
{
    public class CardWork : ICardWork
    {
        IUnitOfWork db;

        public CardWork(IUnitOfWork context)
        {
            db = context;
        }


        public void AddCardToPlayer(Player player, Card card)
        {
            PlayerCard playerCard = new PlayerCard();
            playerCard.Card = card;
            playerCard.Player = player;

            db.PlayerCards.Create(playerCard);
            db.Save();
        }

        public bool DealerBlackJackChecking(Player player)
        {
            bool result = true;
            return result;
        }

        public bool BlackJackChecking(Player player)
        {
            bool result = true;
            return result;
        }

        int PlayerCardSum(Player player)
        {

        }
    }
}
