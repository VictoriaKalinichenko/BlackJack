using AutoMapper;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using BlackJack.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Mappers
{
    public static class CustomMapper
    {
        public static AuthorizePlayerStartView GetAuthorizePlayerStartView(Player human, Game game)
        {
            var authorizePlayerStartView = new AuthorizePlayerStartView()
            {
                PlayerId = human.Id,
                Name = human.Name,
                ResumeGame = true
            };

            if (game == null || !string.IsNullOrEmpty(game.Result))
            {
                authorizePlayerStartView.ResumeGame = false;
            }

            return authorizePlayerStartView;
        }

        public static InitializeRoundStartView GetInitializeRoundStartView(Game game, List<GamePlayer> players, string isGameOver)
        {
            InitializeRoundStartView initializeRoundStartView = Mapper.Map<Game, InitializeRoundStartView>(game);
            initializeRoundStartView.Bots = new List<GamePlayerInitializeRoundStartViewItem>();

            foreach (GamePlayer player in players)
            {
                if (player.Player.Type == PlayerType.Dealer)
                {
                    initializeRoundStartView.Dealer = Mapper.Map<GamePlayer, GamePlayerInitializeRoundStartViewItem>(player);
                }

                if (player.Player.Type == PlayerType.Human)
                {
                    initializeRoundStartView.Human = Mapper.Map<GamePlayer, GamePlayerInitializeRoundStartViewItem>(player);
                }

                if (player.Player.Type == PlayerType.Bot)
                {
                    GamePlayerInitializeRoundStartViewItem bot = Mapper.Map<GamePlayer, GamePlayerInitializeRoundStartViewItem>(player);
                    initializeRoundStartView.Bots.Add(bot);
                }
            }

            initializeRoundStartView.IsGameOver = isGameOver;
            return initializeRoundStartView;
        }

        public static ResponseStartRoundView GetResponseStartRoundView(List<GamePlayer> players, long gameId, bool canTakeCard, bool isBlackJackChoice)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var responseStartRoundView = new ResponseStartRoundView();
            responseStartRoundView.Dealer = Mapper.Map<GamePlayer, GamePlayerResponseStartRoundViewItem>(dealer);
            responseStartRoundView.Dealer.RoundScore = GameValueHelper.Zero;
            responseStartRoundView.Dealer.Cards.Clear();
            responseStartRoundView.Dealer.Cards.Add(dealer.PlayerCards[0].Card.ToString());
            responseStartRoundView.Human = Mapper.Map<GamePlayer, GamePlayerResponseStartRoundViewItem>(human);
            responseStartRoundView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerResponseStartRoundViewItem>>(players);
            responseStartRoundView.CanTakeCard = canTakeCard;
            responseStartRoundView.BlackJackChoice = isBlackJackChoice;
            responseStartRoundView.Id = gameId;
            return responseStartRoundView;
        }

        public static ResponseContinueRoundView GetResponseContinueRoundView(List<GamePlayer> players, long gameId, string humanRoundResult)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var responseContinueRoundView = new ResponseContinueRoundView();
            responseContinueRoundView.Dealer = Mapper.Map<GamePlayer, GamePlayerResponseContinueRoundViewItem>(dealer);
            responseContinueRoundView.Human = Mapper.Map<GamePlayer, GamePlayerResponseContinueRoundViewItem>(human);
            responseContinueRoundView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerResponseContinueRoundViewItem>>(players);
            responseContinueRoundView.RoundResult = humanRoundResult;
            responseContinueRoundView.Id = gameId;
            return responseContinueRoundView;
        }

        public static GetGameHistoryView GetGameHistoryView(IEnumerable<HistoryMessage> historyMessages)
        {
            var getGameHistoryView = new GetGameHistoryView();
            getGameHistoryView.HistoryMessages = Mapper.Map<IEnumerable<HistoryMessage>, List<HistoryMessageGetGameHistoryViewItem>>(historyMessages);
            return getGameHistoryView;
        }

        public static Player GetPlayer(string name, PlayerType playerType)
        {
            var player = new Player();
            player.Name = name;
            player.Type = playerType;
            return player;
        }

        public static GamePlayer GetGamePlayer(Player player, long gameId)
        {
            var gamePlayer = new GamePlayer()
            {
                GameId = gameId,
                PlayerId = player.Id,
                Player = player,
                Score = GameValueHelper.DefaultPlayerScore,
                BetPayCoefficient = BetValueHelper.DefaultCoefficient,
                Bet = GameValueHelper.Zero,
                RoundScore = GameValueHelper.Zero
            };

            return gamePlayer;
        }
        
        public static PlayerCard GetPlayerCard(GamePlayer gamePlayer, Card card)
        {
            var playerCard = new PlayerCard() { GamePlayerId = gamePlayer.Id, CardId = card.Id, Card = card };
            return playerCard;
        }
    }
}
