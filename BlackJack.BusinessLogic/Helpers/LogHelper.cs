using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Enums;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Helpers
{
    public static class LogHelper
    {
        public static List<Log> GetCreationGameLogs(List<GamePlayer> gamePlayers, Game game)
        {
            var logs = new List<Log>();

            logs.Add(new Log()
            {
                GameId = game.Id,
                Message = $"Game(Id={game.Id}, Stage={game.Stage}) is created"
            });

            foreach(GamePlayer gamePlayer in gamePlayers)
            {
                logs.Add(new Log()
                {
                    GameId = gamePlayer.GameId,
                    Message = $@"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, 
                                  Name={gamePlayer.Player.Name}, Score={gamePlayer.Score}) is added to game"
                });
            }

            return logs;
        }

        public static List<Log> GetStartRoundLogs(List<GamePlayer> gamePlayers, long gameId)
        {
            var logs = new List<Log>();
            
            logs.Add(new Log()
            {
                GameId = gameId,
                Message = "New round is started"
            });

            List<Log> betLogs = GetBetsCreationLogs(gamePlayers);
            logs.AddRange(betLogs);

            List<Log> cardLogs = GetCardsAddingLogsAfterStartRound(gamePlayers);
            logs.AddRange(cardLogs);

            List<Log> payCoefficientLogs = GetPayCoefficientDefiningLogsAfterStartRound(gamePlayers);
            logs.AddRange(payCoefficientLogs);
                        
            logs.Add(new Log()
            {
                GameId = gameId,
                Message = $"Stage is changed (Stage={(int)GameStage.StartRound})"
            });

            return logs;
        }

        public static List<Log> GetContinueRoundLogs(List<GamePlayer> gamePlayers, List<PlayerCard> playerCards, long gameId)
        {
            var logs = new List<Log>();

            List<Log> cardLogs = GetCardsAddingLogsAfterContinueRound(gamePlayers, playerCards);
            logs.AddRange(cardLogs);

            List<Log> payCoefficientLogs = GetPayCoefficientDefiningLogsAfterContinueRound(gamePlayers);
            logs.AddRange(payCoefficientLogs);

            logs.Add(new Log()
            {
                GameId = gameId,
                Message = $"Stage is changed (Stage={(int)GameStage.ContinueRound})"
            });

            return logs;
        }
        
        private static List<Log> GetBetsCreationLogs(List<GamePlayer> gamePlayers)
        {
            var logs = new List<Log>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                logs.Add(new Log()
                {
                    GameId = gamePlayer.GameId,
                    Message = $@"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, 
                                  Name={gamePlayer.Player.Name}, Score={gamePlayer.Score}) created the bet(={gamePlayer.Bet})"
                });
            }

            return logs;
        }

        private static List<Log> GetCardsAddingLogsAfterStartRound(List<GamePlayer> gamePlayers)
        {
            var logs = new List<Log>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                logs.Add(GetCardAddingLog(gamePlayer, gamePlayer.PlayerCards[0].Card));
                logs.Add(GetCardAddingLog(gamePlayer, gamePlayer.PlayerCards[1].Card));
            }

            return logs;
        }

        private static List<Log> GetPayCoefficientDefiningLogsAfterStartRound(List<GamePlayer> gamePlayers)
        {
            var logs = new List<Log>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (gamePlayer.BetPayCoefficient == BetValueHelper.BlackJackCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has Blackjack(RoundScore={gamePlayer.RoundScore}). 
                                      BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.WinCoefficient)
                {
                    var log = new Log()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has Blackjack(RoundScore={gamePlayer.RoundScore}) 
                                      with DealerBlackJackDanger. BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    };
                }
            }

            return logs;
        }

        private static List<Log> GetCardsAddingLogsAfterContinueRound(List<GamePlayer> gamePlayers, List<PlayerCard> playerCardsInserted)
        {
            var logs = new List<Log>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                List<PlayerCard> playerCards = playerCardsInserted.Where(m => m.GamePlayerId == gamePlayer.Id).ToList();
                List<Log> gamePlayerLogs = GetCardsAddingLogsForPlayerAfterContinueRound(gamePlayer, playerCards);
                logs.AddRange(gamePlayerLogs);
            }

            return logs;
        }

        private static List<Log> GetCardsAddingLogsForPlayerAfterContinueRound(GamePlayer gamePlayer, List<PlayerCard> playerCards)
        {
            var logs = new List<Log>();

            foreach (PlayerCard playerCard in playerCards)
            {
                GetCardAddingLog(gamePlayer, playerCard.Card);
            }

            return logs;
        }

        private static List<Log> GetPayCoefficientDefiningLogsAfterContinueRound(List<GamePlayer> gamePlayers)
        {
            var logs = new List<Log>();

            GamePlayer dealer = gamePlayers.Where(m => m.Player.Type == (int)PlayerType.Dealer).First();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (gamePlayer.BetPayCoefficient == BetValueHelper.BlackJackCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has Blackjack(RoundScore={gamePlayer.RoundScore}). 
                                      BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.WinCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has win result(PlayerRoundScore={gamePlayer.RoundScore}, 
                                      DealerRoundScore={dealer.RoundScore}). BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.LoseCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has lose result(PlayerRoundScore={gamePlayer.RoundScore}, 
                                      DealerRoundScore={dealer.RoundScore}). BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.ZeroCoefficient)
                {
                    logs.Add(new Log()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has equal result(PlayerRoundScore={gamePlayer.RoundScore}, 
                                      DealerRoundScore={dealer.RoundScore}). BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }
            }

            return logs;
        }

        private static Log GetCardAddingLog(GamePlayer gamePlayer, Card card)
        {
            var log = new Log()
            {
                GameId = gamePlayer.GameId,
                Message = $@"Card(Id={card.Id}, Value={card.Name}, Name={ToStringHelper.GetCardName(card)}) is added to 
                             {((PlayerType)gamePlayer.Player.Type).ToString()}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name})"
            };

            return log;
        }
    }
}
