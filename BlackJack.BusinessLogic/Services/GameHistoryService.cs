using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class GameHistoryService : IGameHistoryService
    {
        private readonly IHistoryMessageRepository _historyMessageRepository;
        
        public GameHistoryService(IHistoryMessageRepository historyMessageRepository)
        {
            _historyMessageRepository = historyMessageRepository;
        }

        public async Task<GetGameHistoryView> Get()
        {
            IEnumerable<HistoryMessage> historyMessages = await _historyMessageRepository.GetAll();
            GetGameHistoryView getGameHistoryView = CustomMapper.GetGameHistoryView(historyMessages);
            return getGameHistoryView;
        }
    }
}
