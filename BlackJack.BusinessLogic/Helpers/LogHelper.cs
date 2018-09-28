using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using System.Collections.Generic;

namespace BlackJack.BusinessLogic.Helpers
{
    public class LogHelper
    {
        private List<Log> _logs;
        private ILogRepository _logRepository;

        public LogHelper(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void BetCreated(GamePlayer gamePlayer)
        {
            string message = $"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name}, Score={gamePlayer.Score}) created the bet(={gamePlayer.Bet})";
            
        }

        public void GameCreated(Game game)
        {
            string message = $"Game(Id={game.Id}, Stage={game.Stage}) is created";
            var log = new Log() { GameId = game.Id, Message = message };
            _logs.Add(log);
        }

        public void PlayerAddedToGame(GamePlayer gamePlayer)
        {
            string message = $"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name}, Score={gamePlayer.Score}) is added to game";
            var log = new Log() { GameId = gamePlayer.GameId, Message = message };
            _logs.Add(log);
        }

        public void GameStageChanged(Game game)
        {
            string message = $"Stage is changed (Stage={game.Stage})";
            var log = new Log() { GameId = game.Id, Message = message };
            _logs.Add(log);
        }

        public void NewRoundStarted(long gameId)
        {
            string message = "New round is started";
            var log = new Log() { GameId = gameId, Message = message };
            _logs.Add(log);
        }

        public void DealerBlackJackDanger(Player dealer, Card dealerFirstCard, long gameId)
        {
            string message = $"Dealer(Id={dealer.Id}, Name={dealer.Name}) has BlackJackDanger. His first card is (Id={dealerFirstCard.Id}, Value={dealerFirstCard.Name}, Name={ CardToStringHelper.Convert(dealerFirstCard) })";
            var log = new Log() { GameId = gameId, Message = message };
            _logs.Add(log);
        }

        public void PlayerBlackJackResult(GamePlayer gamePlayer)
        {
            string message = $"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name}) has Blackjack(RoundScore={gamePlayer.RoundScore}). BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})";
            var log = new Log() { GameId = gamePlayer.GameId, Message = message };
            _logs.Add(log);
        }

        public void PlayerWinResult(GamePlayer player, int dealerRoundScore)
        {
            string message = $"{((PlayerType)player.Player.Type).ToString()}(Id={player.Player.Id}, Name={player.Player.Name}) has win result(PlayerRoundScore={player.RoundScore}, DealerRoundScore={dealerRoundScore}). BetPayCoefficient is changed(={player.BetPayCoefficient})";
            var log = new Log() { GameId = player.GameId, Message = message };
            _logs.Add(log);
        }

        public void PlayerEqualResult(GamePlayer player, int dealerRoundScore)
        {
            string message = $"{((PlayerType)player.Player.Type).ToString()}(Id={player.Player.Id}, Name={player.Player.Name}) has equal result(PlayerRoundScore={player.RoundScore}, DealerRoundScore={dealerRoundScore}). BetPayCoefficient is changed(={player.BetPayCoefficient})";
            var log = new Log() { GameId = player.GameId, Message = message };
            _logs.Add(log);
        }

        public void PlayerLoseResult(GamePlayer player, int dealerRoundScore)
        {
            string message = $"{((PlayerType)player.Player.Type).ToString()}(Id={player.Player.Id}, Name={player.Player.Name}) has lose result(PlayerRoundScore={player.RoundScore}, DealerRoundScore={dealerRoundScore}). BetPayCoefficient is changed(={player.BetPayCoefficient})";
            var log = new Log() { GameId = player.GameId, Message = message };
            _logs.Add(log);
        }

        public void PlayerBlackJackAndDealerBlackJackDanger(GamePlayer gamePlayer)
        {
            string message = $"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name}) has Blackjack(RoundScore={gamePlayer.RoundScore}) with DealerBlackJackDanger. BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})";
            var log = new Log() { GameId = gamePlayer.GameId, Message = message };
            _logs.Add(log);
        }

        public void CardAdded(Player player, Card card, long gameId)
        {
            string message = $"Card(Id={card.Id}, Value={card.Name}, Name={CardToStringHelper.Convert(card)}) is added to {((PlayerType)player.Type).ToString()}(Id={player.Id}, Name={player.Name})";
            var log = new Log() { GameId = gameId, Message = message };
            _logs.Add(log);
        }

      //  public void 

        private void Add(long gameId, string message)
        {
            var log = new Log() { GameId = gameId, Message = message };
            _logs.Add(log);

            if (_logs.Count == GameValueHelper.MaxLogBunch)
            {
                _logRepository.CreateMany(_logs);
                _logs.Clear();
            }
        }
    }
}
