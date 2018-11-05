using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using System.Collections.Generic;
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
            var historyMessages = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                string playerType = gamePlayer.Player.Type.ToString();
                string message = $"{playerType}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name}) is added to game";
                HistoryMessage historyMessage = CustomMapper.GetHistoryMessage(gamePlayer.GameId, message);
                historyMessages.Add(historyMessage);
            }

            await _historyMessageRepository.CreateMany(historyMessages);
        }

        public async Task AddMessagesForRound(List<GamePlayer> gamePlayers, string roundResult, long gameId)
        {
            var historyMessages = new List<HistoryMessage>();

            List<HistoryMessage> cardHistoryMessages = CreateMessagesForAddCards(gamePlayers);
            historyMessages.AddRange(cardHistoryMessages);

            string message = $"RoundResult: {roundResult}";
            HistoryMessage historyMessage = CustomMapper.GetHistoryMessage(gameId, message);
            historyMessages.Add(historyMessage);

            await _historyMessageRepository.CreateMany(historyMessages);
        }
        
        private List<HistoryMessage> CreateMessagesForAddCards(List<GamePlayer> gamePlayers)
        {
            var historyMessages = new List<HistoryMessage>();

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                var historyMessagesForPlayer = new List<HistoryMessage>();
                historyMessagesForPlayer = CreateMessagesForAddCardsPerPlayer(gamePlayer);
                historyMessages.AddRange(historyMessagesForPlayer);
            }

            return historyMessages;
        }

        private List<HistoryMessage> CreateMessagesForAddCardsPerPlayer(GamePlayer gamePlayer)
        {
            var historyMessages = new List<HistoryMessage>();

            foreach (PlayerCard playerCard in gamePlayer.PlayerCards)
            {
                string playerType = gamePlayer.Player.Type.ToString();
                string cardName = playerCard.Card.ToString();
                string message = $@"Card(Id={playerCard.Card.Id}, Value={playerCard.Card.Worth}, Name={cardName}) is added to 
                             {playerType}(Id={gamePlayer.Player.Id}, Name={gamePlayer.Player.Name})";

                HistoryMessage historyMessage = CustomMapper.GetHistoryMessage(gamePlayer.GameId, message);
                historyMessages.Add(historyMessage);
            }

            return historyMessages;
        }
    }
}
