using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using AutoMapper;

namespace BlackJack.BusinessLogic.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<GetLogsViewModel>> GetAll()
        {
            IEnumerable<Log> logs = await _logRepository.GetAll();
            IEnumerable<GetLogsViewModel> logViewModels = Mapper.Map<IEnumerable<Log>, IEnumerable<GetLogsViewModel>>(logs);
            return logViewModels;
        }
    }
}
