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
    public class DealersCards : ICards, IDealersCards
    {
        IUnitOfWork db;
        
        Player dealer;

        IDeck deck;


        public DealersCards(IUnitOfWork context, Player _dealer, IDeck _deck)
        {
            db = context;
            
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
            List<PlayerCard> playerCards = dealer.PlayerCardList;

            foreach (PlayerCard playerCard in playerCards)
            {
                db.PlayerCards.Delete(playerCard.Id);
            }

            db.Save();
        }


        public bool Equals21()
        {
            bool result = false;

            if (dealer.RoundScore == 21)
            {
                result = true;
            }

            return result;
        }

        public bool MoreThan21()
        {
            bool result = false;

            if (dealer.RoundScore > 21)
            {
                result = true;
            }

            return result;
        }


        public bool BJDanger()
        {
            bool danger = false;
            
            PlayerCard playerCard = dealer.PlayerCardList.First();
            if (playerCard.Card.Value >= 10)
            {
                danger = true;
            }

            return danger;
        }



        void PlayerCardAdd(Card card)
        {
            PlayerCard playerCard = new PlayerCard();
            playerCard.Card = card;
            playerCard.Player = dealer;

            db.PlayerCards.Create(playerCard);
            db.Save();
        }

        void RoundScoreUpdate()
        {
            int cardSum = dealer.PlayerCardList.Sum(m => m.Card.Value);
            int AceCount = dealer.PlayerCardList
                .Where(m => m.Card.Name == "Ace")
                .Count();

            while(AceCount > 0 && cardSum > 21)
            {
                AceCount--;
                cardSum -= 10;
            }

            dealer.RoundScore = cardSum;
            db.Players.Update(dealer);
            db.Save();
        }
    }
}
