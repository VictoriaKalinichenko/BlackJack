using AutoMapper;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using BlackJack.ViewModels.GameHistory;
using BlackJack.ViewModels.Round;
using BlackJack.ViewModels.Start;
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

            if (game == null)
            {
                authorizePlayerStartView.ResumeGame = false;
            }

            return authorizePlayerStartView;
        }
        
        public static InitializeStartView GetInitializeStartView(Game game, string userName)
        {
            InitializeStartView initializeStartView = Mapper.Map<Game, InitializeStartView>(game);
            initializeStartView.UserName = userName;
            return initializeStartView;
        }

        public static StartRoundView GetStartRoundView(List<GamePlayer> players, long gameId, bool canTakeCard)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var startRoundView = new StartRoundView();
            startRoundView.Dealer = Mapper.Map<GamePlayer, GamePlayerStartRoundViewItem>(dealer);
            startRoundView.Human = Mapper.Map<GamePlayer, GamePlayerStartRoundViewItem>(human);
            startRoundView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerStartRoundViewItem>>(players);
            startRoundView.CanTakeCard = canTakeCard;
            startRoundView.Id = gameId;
            return startRoundView;
        }

        public static ContinueRoundView GetContinueRoundView(List<GamePlayer> players, long gameId, string humanRoundResult)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var continueRoundView = new ContinueRoundView();
            continueRoundView.Dealer = Mapper.Map<GamePlayer, GamePlayerContinueRoundViewItem>(dealer);
            continueRoundView.Human = Mapper.Map<GamePlayer, GamePlayerContinueRoundViewItem>(human);
            continueRoundView.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerContinueRoundViewItem>>(players);
            continueRoundView.RoundResult = humanRoundResult;
            continueRoundView.Id = gameId;
            return continueRoundView;
        }

        public static RestoreRoundView GetRestoreRoundView(List<GamePlayer> players, long gameId, bool canTakeCard)
        {
            StartRoundView startRoundView = GetStartRoundView(players, gameId, canTakeCard);
            RestoreRoundView restoreRoundView = Mapper.Map<StartRoundView, RestoreRoundView>(startRoundView);
            return restoreRoundView;
        }

        public static GetGameHistoryView GetGameHistoryView(List<HistoryMessage> historyMessages)
        {
            var getGameHistoryView = new GetGameHistoryView();
            getGameHistoryView.HistoryMessages = Mapper.Map<List<HistoryMessage>, List<HistoryMessageGetGameHistoryViewItem>>(historyMessages);
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
                Player = player
            };

            return gamePlayer;
        }
        
        public static List<PlayerCard> GetPlayerCards(GamePlayer gamePlayer, List<Card> cards)
        {
            var playerCards = new List<PlayerCard>();

            foreach (Card card in cards)
            {
                var playerCard = new PlayerCard()
                {
                    CardId = card.Id,
                    GamePlayerId = gamePlayer.Id
                };

                playerCards.Add(playerCard);
            }

            return playerCards;
        }

        public static PlayerCard GetPlayerCard(GamePlayer gamePlayer, Card card)
        {
            var playerCard = new PlayerCard()
            {
                CardId = card.Id,
                GamePlayerId = gamePlayer.Id
            };

            return playerCard;
        }
    }
}
