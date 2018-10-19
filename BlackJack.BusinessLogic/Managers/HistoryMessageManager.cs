using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Managers
{
    public class HistoryMessageManager : IHistoryMessageManager
    {
        private readonly IHistoryMessageRepository _historyMessageRepository;

        public HistoryMessageManager(IHistoryMessageRepository historyMessageRepository)
        {
            _historyMessageRepository = historyMessageRepository;
        }

        public async Task AddCreationGameMessages(List<GamePlayer> gamePlayers, Game game)
        {
            var logs = new List<HistoryMessage>();

            logs.Add(new HistoryMessage()
            {
                GameId = game.Id,
                Message = $"Game(Id={game.Id}, Stage={game.Stage}) is created"
            });

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                logs.Add(new HistoryMessage()
                {
                    GameId = gamePlayer.GameId,
                    Message = $@"{gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, 
                                  Name={gamePlayer.Player.Name}, Score={gamePlayer.Score}) is added to game"
                });
            }

            await _historyMessageRepository.CreateMany(logs);
        }

        public async Task AddStartRoundMessages(List<GamePlayer> gamePlayers, long gameId)
        {
            var logs = new List<HistoryMessage>();

            logs.Add(new HistoryMessage()
            {
                GameId = gameId,
                Message = "New round is started"
            });

            List<HistoryMessage> betLogs = CreateBetCreationMessages(gamePlayers);
            logs.AddRange(betLogs);

            List<HistoryMessage> cardLogs = CreateCardAddingMessagesAfterStartRound(gamePlayers);
            logs.AddRange(cardLogs);

            List<HistoryMessage> payCoefficientLogs = CreatePayCoefficientDefiningMessagesAfterStartRound(gamePlayers);
            logs.AddRange(payCoefficientLogs);

            logs.Add(new HistoryMessage()
            {
                GameId = gameId,
                Message = $"Stage is changed (Stage={(int)GameStage.StartRound})"
            });

            await _historyMessageRepository.CreateMany(logs);
        }

        public async Task AddContinueRoundMessages(List<GamePlayer> gamePlayers, List<PlayerCard> playerCards, long gameId)
        {
            var logs = new List<HistoryMessage>();

            List<HistoryMessage> cardLogs = CreateCardAddingMessagesAfterContinueRound(gamePlayers, playerCards);
            logs.AddRange(cardLogs);

            List<HistoryMessage> payCoefficientLogs = CreatePayCoefficientDefiningMessagesAfterContinueRound(gamePlayers);
            logs.AddRange(payCoefficientLogs);

            logs.Add(new HistoryMessage()
            {
                GameId = gameId,
                Message = $"Stage is changed (Stage={(int)GameStage.ContinueRound})"
            });

            await _historyMessageRepository.CreateMany(logs);
        }

        private List<HistoryMessage> CreateBetCreationMessages(List<GamePlayer> gamePlayers)
        {
            var logs = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                logs.Add(new HistoryMessage()
                {
                    GameId = gamePlayer.GameId,
                    Message = $@"{gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, 
                                  Name={gamePlayer.Player.Name}, Score={gamePlayer.Score}) created the bet(={gamePlayer.Bet})"
                });
            }

            return logs;
        }

        private List<HistoryMessage> CreateCardAddingMessagesAfterStartRound(List<GamePlayer> gamePlayers)
        {
            var logs = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                logs.Add(CreateCardAddingMessage(gamePlayer, gamePlayer.PlayerCards[0].Card));
                logs.Add(CreateCardAddingMessage(gamePlayer, gamePlayer.PlayerCards[1].Card));
            }

            return logs;
        }

        private List<HistoryMessage> CreatePayCoefficientDefiningMessagesAfterStartRound(List<GamePlayer> gamePlayers)
        {
            var logs = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (gamePlayer.BetPayCoefficient == BetValueHelper.BlackJackCoefficient)
                {
                    logs.Add(new HistoryMessage()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has Blackjack(RoundScore={gamePlayer.RoundScore}). 
                                      BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.WinCoefficient)
                {
                    var log = new HistoryMessage()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has Blackjack(RoundScore={gamePlayer.RoundScore}) 
                                      with DealerBlackJackDanger. BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    };
                }
            }

            return logs;
        }

        private List<HistoryMessage> CreateCardAddingMessagesAfterContinueRound(List<GamePlayer> gamePlayers, List<PlayerCard> playerCardsInserted)
        {
            var logs = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                List<PlayerCard> playerCards = playerCardsInserted.Where(m => m.GamePlayerId == gamePlayer.Id).ToList();
                List<HistoryMessage> gamePlayerLogs = CreateCardAddingMessagesForPlayerAfterContinueRound(gamePlayer, playerCards);
                logs.AddRange(gamePlayerLogs);
            }

            return logs;
        }

        private List<HistoryMessage> CreateCardAddingMessagesForPlayerAfterContinueRound(GamePlayer gamePlayer, List<PlayerCard> playerCards)
        {
            var logs = new List<HistoryMessage>();

            foreach (PlayerCard playerCard in playerCards)
            {
                CreateCardAddingMessage(gamePlayer, playerCard.Card);
            }

            return logs;
        }

        private List<HistoryMessage> CreatePayCoefficientDefiningMessagesAfterContinueRound(List<GamePlayer> gamePlayers)
        {
            var logs = new List<HistoryMessage>();

            GamePlayer dealer = gamePlayers.Where(m => m.Player.Type == PlayerType.Dealer).First();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (gamePlayer.BetPayCoefficient == BetValueHelper.BlackJackCoefficient)
                {
                    logs.Add(new HistoryMessage()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has Blackjack(RoundScore={gamePlayer.RoundScore}). 
                                      BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.WinCoefficient)
                {
                    logs.Add(new HistoryMessage()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has win result(PlayerRoundScore={gamePlayer.RoundScore}, 
                                      DealerRoundScore={dealer.RoundScore}). BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.LoseCoefficient)
                {
                    logs.Add(new HistoryMessage()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has lose result(PlayerRoundScore={gamePlayer.RoundScore}, 
                                      DealerRoundScore={dealer.RoundScore}). BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }

                if (gamePlayer.BetPayCoefficient == BetValueHelper.ZeroCoefficient)
                {
                    logs.Add(new HistoryMessage()
                    {
                        GameId = gamePlayer.GameId,
                        Message = $@"{gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, 
                                      Name={gamePlayer.Player.Name}) has equal result(PlayerRoundScore={gamePlayer.RoundScore}, 
                                      DealerRoundScore={dealer.RoundScore}). BetPayCoefficient is changed(={gamePlayer.BetPayCoefficient})"
                    });
                }
            }

            return logs;
        }

        private HistoryMessage CreateCardAddingMessage(GamePlayer gamePlayer, Card card)
        {
            var log = new HistoryMessage()
            {
                GameId = gamePlayer.GameId,
                Message = $@"Card(Id={card.Id}, Value={card.Rank}, Name={card.ToString()}) is added to 
                             {gamePlayer.Player.Type.ToString()}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name})"
            };

            return log;
        }
    }
}
