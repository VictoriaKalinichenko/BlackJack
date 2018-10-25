using AutoMapper;
using BlackJack.BusinessLogic.Constants;
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

        public static InitializeStartView GetInitializeStartView(Game game, List<GamePlayer> players, string isGameOver)
        {
            InitializeStartView initializeStartView = Mapper.Map<Game, InitializeStartView>(game);
            initializeStartView.Bots = new List<GamePlayerInitializeStartViewItem>();

            foreach (GamePlayer player in players)
            {
                if (player.Player.Type == PlayerType.Dealer)
                {
                    initializeStartView.Dealer = Mapper.Map<GamePlayer, GamePlayerInitializeStartViewItem>(player);
                }

                if (player.Player.Type == PlayerType.Human)
                {
                    initializeStartView.Human = Mapper.Map<GamePlayer, GamePlayerInitializeStartViewItem>(player);
                }

                if (player.Player.Type == PlayerType.Bot)
                {
                    GamePlayerInitializeStartViewItem bot = Mapper.Map<GamePlayer, GamePlayerInitializeStartViewItem>(player);
                    initializeStartView.Bots.Add(bot);
                }
            }

            initializeStartView.IsGameOver = isGameOver;
            return initializeStartView;
        }

        public static ResponseStartRoundView GetResponseStartRoundView(List<GamePlayer> players, long gameId, bool canTakeCard, bool isBlackJackChoice)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var responseStartRoundView = new ResponseStartRoundView();
            responseStartRoundView.Dealer = Mapper.Map<GamePlayer, GamePlayerStartRoundViewItem>(dealer);
            responseStartRoundView.Dealer.RoundScore = 0;
            responseStartRoundView.Dealer.Cards.Clear();
            responseStartRoundView.Dealer.Cards.Add(dealer.PlayerCards[0].Card.ToString());
            responseStartRoundView.Human = Mapper.Map<GamePlayer, GamePlayerStartRoundViewItem>(human);
            responseStartRoundView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerStartRoundViewItem>>(players);
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
            responseContinueRoundView.Dealer = Mapper.Map<GamePlayer, GamePlayerContinueRoundViewItem>(dealer);
            responseContinueRoundView.Human = Mapper.Map<GamePlayer, GamePlayerContinueRoundViewItem>(human);
            responseContinueRoundView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerContinueRoundViewItem>>(players);
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
                Score = GameValue.DefaultPlayerScore,
                BetPayCoefficient = BetValue.DefaultCoefficient
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
