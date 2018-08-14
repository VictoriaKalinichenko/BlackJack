using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.Configuration;

namespace BlackJack.BLL.Providers.GamePlayerProviders
{
    public class GamePlayerProvider : IGamePlayerProvider
    {
        public void Create(int playerId, int gameId)
        {
            GamePlayer gamePlayer = new GamePlayer();
            gamePlayer.GameId = gameId;
            gamePlayer.PlayerId = playerId;

            Config.db.GamePlayers.Create(gamePlayer);
        }

        public void DeletePlayerRelations(int playerId)
        {
            List<GamePlayer> gamePlayers = Config.db.GamePlayers.GetAll().Where(m => m.PlayerId == playerId).ToList();

            foreach(GamePlayer gamePlayer in gamePlayers)
            {

            }
        }

        public void DeleteGameRelations(int gameId)
        {

        }
    }
}
