using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Context;
using BlackJack.Entity;

namespace BlackJack.DAL.Repositories
{
    public class PlayerCardRepository : IPlayerCardRepository
    {
        private DataBaseContext db;

        public PlayerCardRepository(DataBaseContext context)
        {
            db = context;
        }



        public IEnumerable<PlayerCard> SelectAll()
        {
            IEnumerable<PlayerCard> playerCards;
            playerCards = db.PlayerCards;
            return playerCards;
        }

        public PlayerCard Select(int Id)
        {
            PlayerCard playerCard;
            playerCard = db.PlayerCards.Find(Id);
            return playerCard;
        }

        public void Create(PlayerCard obj)
        {
            db.PlayerCards.Add(obj);
        }

        public void Delete(int Id)
        {
            PlayerCard playerCard = db.PlayerCards.Find(Id);
            if (playerCard != null)
                db.PlayerCards.Remove(playerCard);
        }
    }
}
