using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;
using BlackJack.BLL.BetFunctions;
using BlackJack.BLL.GameCreation;
using BlackJack.BLL.Deck;
using BlackJack.BLL.Randomize;


namespace BlackJack.BLL.GameMainClass
{
    public class Game : IGame
    {
        public Player HumanPlayer { get; set; }

        IDeck deck;
        IBetWork betWork;
        IUnitOfWork db;

        public Game(IUnitOfWork repository)
        {
            db = repository;
            betWork = new BetWork(db);
            deck = new NormalDeck(db);
        }
        
        public void Create(string name, int AmountOfBots)
        {
            IGameCreation gameCreation = new NewGameCreation(db);
            HumanPlayer = gameCreation.Create(name, AmountOfBots);

            deck.Create();
        }

        public void CreateBets(int HumanBet)
        {
            betWork.CreateBet(HumanPlayer, HumanBet);

            List<Player> bots = GetBotList();
            IRandomize random = new GameRandomize();
            for (int i = 0; i < bots.Count; i++)
            {
                int bet = random.BetGenerate();
                betWork.CreateBet(bots[i], bet);
            }
        }

        public void AddOneMoreCard(Player player)
        {
            Card card = deck.SelectCard();
            PlayerCardAdd(player, card);

            RoundScoreUpdate(player);
        }

        public void FirstCardsAdding()
        {
            List<Player> players = GetBotList();
            players.Add(GetDealer());
            players.Add(HumanPlayer);

            for (int i = 0; i < players.Count; i++)
            {
                AddOneMoreCard(players[i]);
                AddOneMoreCard(players[i]);
            }
        }

        // Проверка первой карты дилера
        public bool DealerBJDanger()
        {
            bool danger = false;

            Player dealer = GetDealer();
            PlayerCard playerCard = dealer.PlayerCardList.First();
            if (playerCard.Card.Value >= 10)
            {
                danger = true;
            }

            return danger;
        } 

        public bool RoundScoreEguals21(Player player)
        {
            bool result = false;

            if (player.RoundScore == 21)
            {
                result = true;
            }

            return result;
        }

        public bool RoundScoreMoreThan21(Player player)
        {
            bool result = false;

            if (player.RoundScore > 21)
            {
                result = true;
            }

            return result;
        }

        public bool CheckAllBetsZero()
        {
            bool result = false;

            List<Player> players = GetBotList();
            players.Add(HumanPlayer);

            if (players.All(m => m.Bet == 0))
            {
                result = true;
            }

            return result;
        }
        
        public void SecondAddingCardsToBots()
        {
            List<Player> bots = GetBotList();
            for (int i = 0; i < bots.Count; i++)
            {
                while (bots[i].RoundScore < 18)
                {
                    AddOneMoreCard(bots[i]);
                }
            }
        }

        public void SecondAddingCardsToDealer()
        {
            Player dealer = GetDealer();
            while (dealer.RoundScore < 17)
            {
                AddOneMoreCard(dealer);
            }
        }

        public bool DealerScoreEqualsPlayerScore(Player player)
        {
            bool result = false;

            Player dealer = GetDealer();
            if (player.RoundScore == dealer.RoundScore)
            {
                result = true;
            }

            return result;
        }

        public bool PlayerWonDealer(Player player)
        {
            bool result = true;

            Player dealer = GetDealer();
            if (player.RoundScore < dealer.RoundScore && !RoundScoreMoreThan21(dealer))
            {
                result = false;
            }

            return result;
        }




        public Player GetDealer()
        {
            Player dealer;

            dealer = db.Players
                .SelectAll()
                .Where(m => m.GameCode == HumanPlayer.Id.ToString())
                .Where(m => m.IsDealer)
                .First();

            return dealer;
        }

        public List<Player> GetBotList()
        {
            List<Player> bots;

            bots = db.Players
                .SelectAll()
                .Where(m => m.GameCode == HumanPlayer.Id.ToString())
                .Where(m => m.Id != HumanPlayer.Id)
                .ToList();

            return bots;
        }




        void PlayerCardAdd(Player player, Card card)
        {
            PlayerCard playerCard = new PlayerCard();
            playerCard.Card = card;
            playerCard.Player = player;

            db.PlayerCards.Create(playerCard);
            db.Save();
        }

        void RoundScoreUpdate(Player player) // Туз 1 или 11
        {
            int cardSum = player.PlayerCardList.Sum(m => m.Card.Value);

            player.RoundScore = cardSum;
            db.Players.Update(player);
            db.Save();
        }
    }
}
