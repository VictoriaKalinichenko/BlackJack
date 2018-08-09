using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;
using BlackJack.BLL.Randomize;
using BlackJack.BLL.DataChecking;

namespace BlackJack.BLL.GameCreation
{
    public class NewGameCreation : IGameCreation
    {
        private IUnitOfWork db;

        public NewGameCreation(IUnitOfWork repository)
        {
            db = repository;
        }


        #region GameCreation

        private int DefaultPlayerScore = 8000;
        
        public Player Create(string humanPlayerName, int AmountOfBots)
        {
            AddPlayer(humanPlayerName);
            Player humanPlayer = db.Players.SelectByName(humanPlayerName);

            string GameCode = GameCodeDefinition(humanPlayer);

            IRandomize gameRandomize = new GameRandomize();
            IDataChecking NameCheck = new DChecking(db);

            AddBots(AmountOfBots, GameCode, NameCheck, gameRandomize);
            AddDealer(GameCode, NameCheck, gameRandomize);

            return humanPlayer;
        }

        private void AddPlayer(string _Name, string _GameCode = "", bool _IsDealer = false)
        {
            Player player = new Player();
            player.Name = _Name;
            player.GameCode = _GameCode;
            player.Score = DefaultPlayerScore;
            player.IsDealer = _IsDealer;
            
            db.Players.Create(player);
            db.Save();
        }

        void AddBots(int AmountOfBots, string GameCode, IDataChecking NameCheck, IRandomize random)
        {
            for (byte i = 0; i < AmountOfBots; i++)
            {
                string BotName;
                {
                    BotName = random.NameGenerate();
                }
                while (NameCheck.PlayerNameCheck(BotName)) ;
                AddPlayer(BotName, GameCode);
            }
        }

        void AddDealer(string GameCode, IDataChecking NameCheck, IRandomize random)
        {
            string DealerName;
            {
                DealerName = random.NameGenerate();
            }
            while (NameCheck.PlayerNameCheck(DealerName)) ;
            AddPlayer(DealerName, GameCode, true);
        }

        string GameCodeDefinition(Player player)
        {
            player.GameCode = player.Id.ToString();
            
            db.Players.Update(player);
            db.Save();

            return player.GameCode;
        }

        #endregion
    }
}
