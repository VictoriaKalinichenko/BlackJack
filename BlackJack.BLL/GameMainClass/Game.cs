using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.BLL.GameCreation;
using BlackJack.BLL.Deck;
using BlackJack.BLL.Players;


namespace BlackJack.BLL.GameMainClass
{
    public class Game : IGame
    {
        Player humanPlayer;
        Player dealer;
        List<Player> bots;

        BjPlayer bjHumanPlayer;
        BjDealer bjDealer;
        BjBots bjBots;

        IDeck deck;
        IUnitOfWork db;

        public Game(IUnitOfWork repository)
        {
            db = repository;
            deck = new NormalDeck(db);
        }


        public BjPlayer HumanPlayer
        {
            get
            {
                if (bjHumanPlayer == null)
                {
                    bjHumanPlayer = new BjPlayer(db, humanPlayer, dealer, deck);
                }
                return bjHumanPlayer;
            }
        }

        public BjDealer Dealer
        {
            get
            {
                if (bjDealer == null)
                {
                    bjDealer = new BjDealer(db, dealer, deck);
                }
                return bjDealer;
            }
        }

        public BjBots Bots
        {
            get
            {
                if (bjBots == null)
                {
                    bjBots = new BjBots(db, bots, dealer, deck);
                }
                return bjBots;
            }
        }



        public void Create(string name, int AmountOfBots)
        {
            IGameCreation gameCreation = new NewGameCreation(db);
            humanPlayer = gameCreation.Create(name, AmountOfBots);
            BotsAndDealerInitialize();
            deck.Create();
        }

        public void Resume(string name)
        {
            humanPlayer = db.Players.SelectByName(name);
            BotsAndDealerInitialize();
            deck.Resume();
        }



        public void FirstCardsAdding()
        {
            bjHumanPlayer.Cards.FirstCardsTaking();
            bjDealer.Cards.FirstCardsTaking();
            bjBots.FirstCardsAdding();
        }

        public void RemoveAllCards()
        {
            bjHumanPlayer.Cards.RemoveCards();
            bjDealer.Cards.RemoveCards();
            bjBots.RemoveCards();
        }

        public void RoundEnding()
        {
            RemoveAllCards();
        }

        public void GameEnding()
        {
            RemoveAllCards();

            db.Players.Delete(humanPlayer.Id);
            db.Players.Delete(dealer.Id);

            foreach(Player bot in bots)
            {
                db.Players.Delete(bot.Id);
            }
        }



        public bool IsGameOver()
        {
            bool result = false;

            if (bjHumanPlayer.IsScoreNull() || bjDealer.IsScoreNull())
            {
                result = true;
            }

            return result;
        }

        public bool IsRoundOver()
        {
            bool result = false;

            if (bjHumanPlayer.IsBetNull() && bjBots.IsRoundOver())
            {
                result = true;
            }

            return result;
        }



        void BotsAndDealerInitialize()
        {
            dealer = db.Players.SelectAll()
                .Where(m => m.GameCode == humanPlayer.Id.ToString())
                .Where(m => m.IsDealer)
                .First();

            bots = db.Players.SelectAll()
                .Where(m => m.GameCode == humanPlayer.Id.ToString())
                .Where(m => m.IsDealer == false)
                .Where(m => m.Id != humanPlayer.Id)
                .ToList();
        }
    }
}
