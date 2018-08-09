using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Cards;
using BlackJack.DAL.Interfaces;
using BlackJack.BLL.Deck;
using BlackJack.BLL.Bet;

namespace BlackJack.BLL.Players
{
    public class BjPlayer : IPlayer
    {
        Player player;
        Player dealer;
        IUnitOfWork db;
        IDeck deck;

        PlayersCards playersCards;
        BjBet bjBet;


        public BjPlayer(IUnitOfWork context, Player _player, Player _dealer, IDeck _deck)
        {
            db = context;
            player = _player;
            dealer = _dealer;
            deck = _deck;
        }


        public ICardsAgainstDealer Cards
        {
            get
            {
                if (playersCards == null)
                {
                    playersCards = new PlayersCards(db, player, dealer, deck);
                }
                return playersCards;
            }
        }

        public IBet Bet
        {
            get
            {
                if (bjBet == null)
                {
                    bjBet = new BjBet(db, player, dealer);
                }
                return bjBet;
            }
        }


        public bool IsScoreNull()
        {
            bool result = false;

            if (player.Score <= 0)
            {
                result = true;
            }

            return result;
        }

        public bool IsBetNull()
        {
            bool result = false;

            if (player.Bet <= 0)
            {
                result = true;
            }

            return result;
        }

        public Player GetPlayer()
        {
            return player;
        }
    }
}
