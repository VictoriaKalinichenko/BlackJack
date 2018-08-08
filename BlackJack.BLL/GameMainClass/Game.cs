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


namespace BlackJack.BLL.GameMainClass
{
    public class Game : IGame
    {
        public Player HumanPlayer { get; set; }
        List<Card> deck;


        IUnitOfWork db;


        public Game(IUnitOfWork repository)
        {
            db = repository;
        }
        
        public void Create(string name, int AmountOfBots)
        {
            IGameCreation gameCreation = new NewGameCreation(db);
            HumanPlayer = gameCreation.Create(name, AmountOfBots);

            // Инициализация колоды
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
    }
}
