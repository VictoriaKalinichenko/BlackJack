using AutoMapper;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.ViewModels.Log;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<GetAllViewModel>> GetAll()
        {
            IEnumerable<Log> logs = await _logRepository.GetAll();
            IEnumerable<GetAllViewModel> logViews = Mapper.Map<IEnumerable<Log>, IEnumerable<GetAllViewModel>>(logs);
            return logViews;
        }
    }
}
