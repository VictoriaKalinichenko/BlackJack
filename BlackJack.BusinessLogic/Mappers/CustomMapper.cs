using AutoMapper;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using BlackJack.ViewModels.GameHistory;
using BlackJack.ViewModels.Round;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Mappers
{
    public static class CustomMapper
    {        
        public static StartRoundView MapStartRoundView(List<GamePlayer> players, bool canTakeCard)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var view = new StartRoundView();
            view.Dealer = Mapper.Map<GamePlayer, GamePlayerStartRoundViewItem>(dealer);
            view.Human = Mapper.Map<GamePlayer, GamePlayerStartRoundViewItem>(human);
            view.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerStartRoundViewItem>>(players);
            view.CanTakeCard = canTakeCard;
            return view;
        }

        public static EndRoundView MapContinueRoundView(List<GamePlayer> players, string humanRoundResult)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var view = new EndRoundView();
            view.Dealer = Mapper.Map<GamePlayer, GamePlayerContinueRoundViewItem>(dealer);
            view.Human = Mapper.Map<GamePlayer, GamePlayerContinueRoundViewItem>(human);
            view.Bots = Mapper.Map<IEnumerable<GamePlayer>, List<GamePlayerContinueRoundViewItem>>(players);
            view.RoundResult = humanRoundResult;
            return view;
        }

        public static RestoreRoundView MapRestoreRoundView(List<GamePlayer> players, bool canTakeCard)
        {
            StartRoundView startView = MapStartRoundView(players, canTakeCard);
            RestoreRoundView viewForRestoreRound = Mapper.Map<StartRoundView, RestoreRoundView>(startView);
            return viewForRestoreRound;
        }

        public static GetGameHistoryView MapGameHistoryView(List<HistoryMessage> historyMessages)
        {
            var view = new GetGameHistoryView();
            view.HistoryMessages = Mapper.Map<List<HistoryMessage>, List<HistoryMessageGetGameHistoryViewItem>>(historyMessages);
            return view;
        }

        public static Player MapPlayer(string name, PlayerType playerType)
        {
            var player = new Player();
            player.Name = name;
            player.Type = playerType;
            return player;
        }

        public static GamePlayer MapGamePlayer(Player player, long gameId)
        {
            var gamePlayer = new GamePlayer();
            gamePlayer.GameId = gameId;
            gamePlayer.PlayerId = player.Id;
            gamePlayer.Player = player;
            return gamePlayer;
        }
        
        public static List<PlayerCard> MapPlayerCards(GamePlayer gamePlayer, List<Card> cards)
        {
            var playerCards = new List<PlayerCard>();

            foreach (Card card in cards)
            {
                var playerCard = new PlayerCard();
                playerCard.CardId = card.Id;
                playerCard.Card = card;
                playerCard.GamePlayerId = gamePlayer.Id;
                playerCards.Add(playerCard);
            }

            return playerCards;
        }

        public static PlayerCard MapPlayerCard(GamePlayer gamePlayer, Card card)
        {
            var playerCard = new PlayerCard();
            playerCard.CardId = card.Id;
            playerCard.Card = card;
            playerCard.GamePlayerId = gamePlayer.Id;
            return playerCard;
        }

        public static Game MapGameWithIdAndRoundResult(long id, string roundResult)
        {
            var game = new Game();
            game.Id = id;
            game.RoundResult = roundResult;
            return game;
        }

        public static HistoryMessage MapHistoryMessage (long gameId, string message)
        {
            var historyMessage = new HistoryMessage();
            historyMessage.GameId = gameId;
            historyMessage.Message = message;
            return historyMessage;
        }
    }
}
