using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
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

        public async Task AddMessagesForCreateGame(List<GamePlayer> gamePlayers, Game game)
        {
            var logs = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                string playerType = gamePlayer.Player.Type.ToString();
                logs.Add(new HistoryMessage()
                {
                    GameId = gamePlayer.GameId,
                    Message = $"{playerType}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name}) is added to game"
                });
            }

            await _historyMessageRepository.CreateMany(logs);
        }

        public async Task AddMessagesForStartRound(List<GamePlayer> gamePlayers, long gameId)
        {
            var logs = new List<HistoryMessage>();

            logs.Add(new HistoryMessage()
            {
                GameId = gameId,
                Message = "New round is started"
            });
            
            List<HistoryMessage> cardLogs = CreateMessagesForAddCardsForStartRound(gamePlayers);
            logs.AddRange(cardLogs);
            
            await _historyMessageRepository.CreateMany(logs);
        }

        public async Task AddMessagesForContinueRound(List<GamePlayer> gamePlayers, List<PlayerCard> playerCards, string roundResult, long gameId)
        {
            var logs = new List<HistoryMessage>();

            List<HistoryMessage> cardLogs = CreateMessagesForAddCardsForContinueRound(gamePlayers, playerCards);
            logs.AddRange(cardLogs);

            logs.Add(new HistoryMessage()
            {
                GameId = gameId,
                Message = $"RoundResult: {roundResult}"
            });

            await _historyMessageRepository.CreateMany(logs);
        }
        
        private List<HistoryMessage> CreateMessagesForAddCardsForStartRound(List<GamePlayer> gamePlayers)
        {
            var logs = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                logs.Add(CreateMessageForAddCard(gamePlayer, gamePlayer.PlayerCards[0].Card));
                logs.Add(CreateMessageForAddCard(gamePlayer, gamePlayer.PlayerCards[1].Card));
            }

            return logs;
        }
        
        private List<HistoryMessage> CreateMessagesForAddCardsForContinueRound(List<GamePlayer> gamePlayers, List<PlayerCard> createdPlayerCards)
        {
            var logs = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                List<PlayerCard> playerCards = createdPlayerCards.Where(m => m.GamePlayerId == gamePlayer.Id).ToList();
                List<HistoryMessage> gamePlayerLogs = CreateMessagesForAddCardsPerPlayer(gamePlayer, playerCards);
                logs.AddRange(gamePlayerLogs);
            }

            return logs;
        }

        private List<HistoryMessage> CreateMessagesForAddCardsPerPlayer(GamePlayer gamePlayer, List<PlayerCard> playerCards)
        {
            var logs = new List<HistoryMessage>();

            foreach (PlayerCard playerCard in playerCards)
            {
                CreateMessageForAddCard(gamePlayer, playerCard.Card);
            }

            return logs;
        }
        
        private HistoryMessage CreateMessageForAddCard(GamePlayer gamePlayer, Card card)
        {
            string playerType = gamePlayer.Player.Type.ToString();
            string cardName = card.ToString();
            var log = new HistoryMessage()
            {
                GameId = gamePlayer.GameId,
                Message = $@"Card(Id={card.Id}, Value={card.Worth}, Name={cardName}) is added to 
                             {playerType}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name})"
            };

            return log;
        }
    }
}
