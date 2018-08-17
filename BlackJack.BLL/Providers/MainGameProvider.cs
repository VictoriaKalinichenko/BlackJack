using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Providers;
using BlackJack.BLL.Providers.Interfaces;
using BlackJack.BLL.Services;
using BlackJack.BLL.Services.Interfaces;
using BlackJack.BLL.ViewModels;
using BlackJack.ConsoleApp.ConsoleInputOutput.ConsoleInputs;
using BlackJack.ConsoleApp.ConsoleInputOutput.ConsoleOutputs;


namespace BlackJack.BLL.Providers
{
    public class MainGameProvider : IMainGameProvider
    {
        private IGameService GameService;
        
        private IConsoleInput GameInput;
        private IConsoleOutput GameOutput;
        
        private IBetProvider BetProvider;
        private ICardDistributionProvider CardDistributionProvider;
        private ICardCheckProvider CardCheckProvider;
        
        public MainGameProvider()
        {
            GameService = new GameService();

            GameInput = new ConsoleInput();
            GameOutput = new ConsoleOutput();
            
            BetProvider = new BetProvider();
            CardDistributionProvider = new CardDistributionProvider();
            CardCheckProvider = new CardCheckProvider();
        }



        public void Start()
        {
            GameViewModel gameViewModel = Create();
            List<Card> deck = CardDistributionProvider.CreateDeck();

            RoundStart(gameViewModel);
            RoundFirstPhase(gameViewModel, deck);
            OneMoreCardAddingToHuman(gameViewModel, deck);
        }


        private GameViewModel Create()
        {
            GameViewModel gameViewModel;

            string humanName = GameInput.InputName();
            string validationMessage = GameService.PlayerNameValidation(humanName);
            if (validationMessage != "")
            {
                GameOutput.ExitWithMessage(validationMessage);
            }

            int amountOfBots = GameInput.InputAmountOfBots();

            gameViewModel = GameService.CreateGame(humanName, amountOfBots);

            return gameViewModel;
        }

        private void RoundStart(GameViewModel gameViewModel)
        {
            GameOutput.RoundStartPageOutput(gameViewModel.Players);
            int humanBet = GameInput.InputBet();

            BetProvider.BetCreations(gameViewModel.Players, humanBet);
        }

        private void RoundFirstPhase(GameViewModel gameViewModel, List<Card> deck)
        {
            CardDistributionProvider.FirstCardsDistribution(gameViewModel.Players, deck);
            bool bjAndDealerBjDanger = CardCheckProvider.FirstCardCheck(gameViewModel.Players);
            
            GameOutput.RoundFirstPhaseOutput(gameViewModel.Players);
            
            int bjKey = GameInput.InputKeyForBjPayment(bjAndDealerBjDanger);
            BetProvider.RoundBetPayments(gameViewModel.Players, bjKey);

            GameOutput.RoundFirstPhaseOutput(gameViewModel.Players);
        }


        
        private void OneMoreCardAddingToHuman(GameViewModel gameViewModel, List<Card> deck)
        {
            PlayerViewModel human = gameViewModel.Players.Where(m => m.Player.IsHuman).First();

            bool canMoreAdding = CardDistributionProvider.OneMoreCardToHuman(human);
            OneMoreCardLoop(canMoreAdding, gameViewModel, deck);
        }

        private void OneMoreCardLoop(bool canMoreAdding, GameViewModel gameViewModel, List<Card> deck)
        {
            int key = 0;

            if (canMoreAdding)
            {
                PlayerViewModel human = gameViewModel.Players.Where(m => m.Player.IsHuman).First();

                key = GameInput.InputKeyForAddingCard();
                canMoreAdding = CardDistributionProvider.OneMoreCardToHuman(human, deck, key);
                
                GameOutput.RoundFirstPhaseOutput(gameViewModel.Players);
            }

            if (key == 0)
            {
                return;
            }

            if (key == 1)
            {
                OneMoreCardLoop(canMoreAdding, gameViewModel, deck);
            }
        }



        private void RoundSecondPhase()
        {

        }
    }
}
