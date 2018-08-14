using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Configuration;
using BlackJack.Entity;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Providers.DeckProviders;
using BlackJack.BLL.Providers.GamePlayerProviders;
using BlackJack.BLL.Providers.PlayerProviders;
using BlackJack.BLL.Providers.GameProviders;

namespace BlackJack.BLL.Services.GameCreationServices
{
    public class GameCreationService : IGameCreationService
    {
        public Game CreateGame()
        {
            Game game;

            IGameProvider workWithGameInDb = new GameProvider();
            game = workWithGameInDb.CreateInDb();

            return game;
        }

        public List<Player> CreatePlayerList(string humanName, int amountOfBots, int gameId)
        {
            List<Player> players = new List<Player>();

            IPlayerProvider workWithPlayer = new PlayerProvider();
            players.Add(workWithPlayer.CreateInDb(false, true, humanName));
            players.Add(workWithPlayer.CreateInDb(true));

            for (int i = 0; i < amountOfBots; i++)
            {
                players.Add(workWithPlayer.CreateInDb());
            }

            BotsAndDealerNamesGenerating(players);

            IGamePlayerProvider workWithGamePlayerInDb = new GamePlayerProvider();
            for (int i = 0; i < players.Count; i++)
            {
                workWithGamePlayerInDb.Create(players[i].Id, gameId);
            }


            return players;
        }

        public List<Card> CreateDeck()
        {
            List<Card> cards;

            IDeckProvider workWithDeck = new DeckProvider();
            cards = workWithDeck.CreateDeck();

            return cards;
        }

        

        private void BotsAndDealerNamesGenerating(List<Player> players)
        {
            Random random = new Random();

            foreach (Player player in players)
            {
                if (!player.IsHuman)
                {
                    player.Name = BotNames.Names[random.Next(BotNames.Names.Length)] + " " + BotNames.Names[random.Next(BotNames.Names.Length)];
                    Config.db.Players.Update(player);
                }
            }
        }
    }
}
