using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels.Log;
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

        public async Task<IEnumerable<LogGetLogsView>> GetAllLogs()
        {
            IEnumerable<Log> logs = await _logRepository.GetAll();
            IEnumerable<LogGetLogsView> logViews = Mapper.Map<IEnumerable<Log>, IEnumerable<LogGetLogsView>>(logs);
            return logViews;
        }
    }
}
