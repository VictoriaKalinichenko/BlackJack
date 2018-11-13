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
        public static StartRoundView MapStartRoundView(List<GamePlayer> players, string roundResult)
        {
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            players.Remove(human);
            players.Remove(dealer);

            var view = new StartRoundView();
            view.Dealer = Mapper.Map<GamePlayer, GamePlayerStartRoundViewItem>(dealer);
            view.Human = Mapper.Map<GamePlayer, GamePlayerStartRoundViewItem>(human);
            view.Bots = Mapper.Map<List<GamePlayer>, List<GamePlayerStartRoundViewItem>>(players);
            view.RoundResult = roundResult;
            return view;
        }

        public static TakeCardRoundView MapTakeCardRoundView(List<GamePlayer> players, string roundResult)
        {
            StartRoundView viewForStartRound = MapStartRoundView(players, roundResult);
            var viewForTakeCard = Mapper.Map<StartRoundView, TakeCardRoundView>(viewForStartRound);
            return viewForTakeCard;
        }

        public static EndRoundView MapEndRoundView(GamePlayer dealer, string roundResult)
        {
            var view = new EndRoundView();
            view.Dealer = Mapper.Map<GamePlayer, GamePlayerEndRoundViewItem>(dealer);
            view.RoundResult = roundResult;
            return view;
        }

        public static RestoreRoundView MapRestoreRoundView(List<GamePlayer> players, string roundResult)
        {
            StartRoundView viewForStartRound = MapStartRoundView(players, roundResult);
            var viewForRestoreRound = Mapper.Map<StartRoundView, RestoreRoundView>(viewForStartRound);
            return viewForRestoreRound;
        }

        public static GetGameHistoryView MapGameHistoryView(List<HistoryMessage> historyMessages)
        {
            var historyMessageViewItems = Mapper.Map<List<HistoryMessage>, List<HistoryMessageGetGameHistoryViewItem>>(historyMessages);
            var view = new GetGameHistoryView(historyMessageViewItems);
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

        public static Game MapGame(long id, string roundResult)
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
