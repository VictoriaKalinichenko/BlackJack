using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.BLL.Deck;

namespace BlackJack.BLL.Cards
{
    public class PlayersCards : ICardsAgainstDealer, ICards 
    {
        IUnitOfWork db;

        Player player;
        Player dealer;

        IDeck deck;


        public PlayersCards(IUnitOfWork context, Player _player, Player _dealer, IDeck _deck)
        {
            db = context;

            player = _player;
            dealer = _dealer;

            deck = _deck;
        }



        public void TakeCard()
        {
            Card card = deck.SelectCard();
            PlayerCardAdd(card);

            RoundScoreUpdate();
        }

        public void FirstCardsTaking()
        {
            TakeCard();
            TakeCard();
        }

        public void RemoveCards()
        {
            List<PlayerCard> playerCards = player.PlayerCardList;

            foreach (PlayerCard playerCard in playerCards)
            {
                db.PlayerCards.Delete(playerCard.Id);
            }

            db.Save();
        }


        public bool Equals21()
        {
            bool result = false;

            if (player.RoundScore == 21)
            {
                result = true;
            }

            return result;
        }

        public bool MoreThan21()
        {
            bool result = false;

            if (player.RoundScore > 21)
            {
                result = true;
            }

            return result;
        }

        public bool EqualsDealerScore()
        {
            bool result = false;
            
            if (player.RoundScore == dealer.RoundScore)
            {
                result = true;
            }

            return result;
        }

        public bool BetterThanDealerScore()
        {
            bool result = true;
            
            if (player.RoundScore < dealer.RoundScore && dealer.RoundScore < 22)
            {
                result = false;
            }

            return result;
        }



        void PlayerCardAdd(Card card)
        {
            PlayerCard playerCard = new PlayerCard();
            playerCard.Card = card;
            playerCard.Player = player;

            db.PlayerCards.Create(playerCard);
            db.Save();
        }

        void RoundScoreUpdate()  // Туз 1 или 11 -> доделать
        {
            int cardSum = player.PlayerCardList.Sum(m => m.Card.Value);

            player.RoundScore = cardSum;
            db.Players.Update(player);
            db.Save();
        }
    }
}
