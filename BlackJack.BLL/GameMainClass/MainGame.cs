using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Services.GameCreationServices;
using BlackJack.BLL.Providers.ConsoleInputProviders;
using BlackJack.BLL.Providers.PlayerProviders;


namespace BlackJack.BLL.GameMainClass
{
    public class MainGame : IMainGame
    {
        private Game Game;
        private List<Player> Players;
        private List<Card> Deck;

        private Dictionary<int, List<Card>> CardsOnTable;
        private Dictionary<int, RoundResult> RoundResults;

        public void Start()
        {
            Create();
            RoundStart();
        }


        private void Create()
        {
            IGameInputProvider gameInput = new GameInputProvider();
            string humanName = gameInput.InputName();
            int amountOfBots = gameInput.InputAmountOfBots();

            IGameCreationService gameCreation = new GameCreationService();
            Game = gameCreation.CreateGame();
            Players = gameCreation.CreatePlayerList(humanName, amountOfBots, Game.Id);
            Deck = gameCreation.CreateDeck();
        }

        private void RoundStart()
        {
            IPlayerProvider workWithPlayer = new PlayerProvider();
            Player human = workWithPlayer.GetHumanFromList(Players);
        }
    }
}
