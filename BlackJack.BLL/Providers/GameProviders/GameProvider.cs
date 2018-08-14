using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.Configuration;

namespace BlackJack.BLL.Providers.GameProviders
{
    public class GameProvider : IGameProvider
    {
        public Game CreateInDb()
        {
            Game game = new Game();
            game.Id = LastGameId() + 1;

            Config.db.Games.Create(game);

            return game;
        }


        private int LastGameId()
        {
            int gameId = 0;

            Game game = Config.db.Games.GetAll().LastOrDefault();
            if (game != null)
            {
                gameId = game.Id;
            }

            return gameId;
        }
    }
}
