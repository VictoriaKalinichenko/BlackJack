using BlackJack.BusinessLogic.Mappers;
using BlackJack.BusinessLogic.Services.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.GameHistory;
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
            List<HistoryMessage> historyMessages = await _historyMessageRepository.GetAll();
            GetGameHistoryView view = CustomMapper.MapGameHistoryView(historyMessages);
            return view;
        }
    }
}
