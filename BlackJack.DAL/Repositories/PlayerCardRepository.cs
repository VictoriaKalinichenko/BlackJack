using BlackJack.DAL.Context;
using BlackJack.DAL.Interfaces;
using BlackJack.Entity.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.DAL.Repositories
{
    public class PlayerCardRepository : IPlayerCardRepository 
    {
        public IEnumerable<PlayerCard> GetAll()
        {
            IEnumerable<PlayerCard> playerCards;

            using (DataBaseContext db = new DataBaseContext())
            {
                playerCards = db.PlayerCards;
            }

            return playerCards;
        }

        public PlayerCard Get(int id)
        {
            PlayerCard playerCard;

            using (DataBaseContext db = new DataBaseContext())
            {
                playerCard = db.PlayerCards.Find(id);
            }

            return playerCard;
        }

        public void Create(PlayerCard obj)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                db.PlayerCards.Add(obj);
                db.SaveChanges();
            }
        }

        public void DeleteByGamePlayerId(int gamePlayerId)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                foreach (PlayerCard playerCard in db.PlayerCards)
                {
                    if (playerCard.GamePlayerId == gamePlayerId)
                    {
                        Delete(playerCard.Id);
                    }
                }
            }
        }

        private void Delete(int id)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                PlayerCard playerCard = db.PlayerCards.Find(id);
                if (playerCard != null)
                {
                    db.PlayerCards.Remove(playerCard);
                }
                db.SaveChanges();
            }
        }


    }
}
