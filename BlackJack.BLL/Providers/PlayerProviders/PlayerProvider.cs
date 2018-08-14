using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.Configuration;

namespace BlackJack.BLL.Providers.PlayerProviders
{
    public class PlayerProvider : IPlayerProvider
    {
        private int DefaultPlayerScore = 8000;


        public Player CreateInDb(bool IsDealer = false, bool IsHuman = false, string Name = "")
        {
            Player player = new Player();

            player.Id = LastPlayerId() + 1;
            player.Name = Name;
            player.Score = DefaultPlayerScore;
            player.IsDealer = IsDealer;
            player.IsHuman = IsHuman;

            Config.db.Players.Create(player);

            return player;
        }

        public Player GetHumanFromList(List<Player> players)
        {
            Player player;

            player = players.Where(m => m.IsHuman).FirstOrDefault();

            return player;
        }

        public void DeleteInDb(int PlayerId)
        {

        }


        private int LastPlayerId()
        {
            int playerId = 0;

            Player player = Config.db.Players.GetAll().LastOrDefault();
            if (player != null)
            {
                playerId = player.Id;
            }

            return playerId;
        }
    }
}
