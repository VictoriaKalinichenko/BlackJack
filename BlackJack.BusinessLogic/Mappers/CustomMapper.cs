using AutoMapper;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using BlackJack.ViewModels.ViewModels.Game;
using BlackJack.ViewModels.ViewModels.Start;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Mappers
{
    public static class CustomMapper
    {
        public static StartRoundResponseViewModel GetStartRoundResponseViewModel(List<GamePlayer> players, long gameId, bool canTakeCard, bool isBlackJackChoice)
        {
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var startRoundResponseViewModel = new StartRoundResponseViewModel();
            startRoundResponseViewModel.Dealer = Mapper.Map<GamePlayer, GamePlayerItem>(dealer);
            startRoundResponseViewModel.Dealer.RoundScore = GameValueHelper.Zero;
            startRoundResponseViewModel.Dealer.Cards.Clear();
            startRoundResponseViewModel.Dealer.Cards.Add(CardToStringHelper.Convert(dealer.PlayerCards[0].Card));
            startRoundResponseViewModel.Human = Mapper.Map<GamePlayer, GamePlayerItem>(human);
            startRoundResponseViewModel.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerItem>>(players);
            startRoundResponseViewModel.CanTakeCard = canTakeCard;
            startRoundResponseViewModel.BlackJackChoice = isBlackJackChoice;
            startRoundResponseViewModel.Id = gameId;
            return startRoundResponseViewModel;
        }

        public static ContinueRoundResponseViewModel GetContinueRoundResponseViewModel(List<GamePlayer> players, long gameId, string humanRoundResult)
        {
            GamePlayer human = players.Where(m => m.Player.Type == (int)PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var continueRoundResponseViewModel = new ContinueRoundResponseViewModel();
            continueRoundResponseViewModel.Dealer = Mapper.Map<GamePlayer, GamePlayerItem>(dealer);
            continueRoundResponseViewModel.Human = Mapper.Map<GamePlayer, GamePlayerItem>(human);
            continueRoundResponseViewModel.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerItem>>(players);
            continueRoundResponseViewModel.RoundResult = humanRoundResult;
            continueRoundResponseViewModel.Id = gameId;
            return continueRoundResponseViewModel;
        }

        public static InitRoundViewModel GetInitRoundViewModel(Game game, List<GamePlayer> bots, GamePlayer human, GamePlayer dealer, string isGameOver)
        {
            InitRoundViewModel initRoundViewModel = Mapper.Map<Game, InitRoundViewModel>(game);
            initRoundViewModel.Dealer = Mapper.Map<GamePlayer, PlayerItem>(dealer);
            initRoundViewModel.Human = Mapper.Map<GamePlayer, PlayerItem>(human);
            initRoundViewModel.Bots = Mapper.Map<List<GamePlayer>, List<PlayerItem>>(bots);
            initRoundViewModel.IsGameOver = isGameOver;
            return initRoundViewModel;
        }
    }
}
