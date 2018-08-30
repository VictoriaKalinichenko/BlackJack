using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using NLog;

namespace BlackJack.BusinessLogic.Services
{
    public class LogService : ILogService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ILogRepository _logRepository;
        
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<LogViewModel>> GetAll()
        {
            try
            {
                IEnumerable<Log> logs = await _logRepository.GetAll();
                IEnumerable<LogViewModel> logViewModels = LogToLogViewModel(logs);
                return logViewModels;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private IEnumerable<LogViewModel> LogToLogViewModel(IEnumerable<Log> logs)
        {
            try
            {
                List<LogViewModel> logViewModels = new List<LogViewModel>();

                foreach (Log log in logs)
                {
                    LogViewModel logViewModel = new LogViewModel();
                    logViewModel.Id = log.Id;
                    logViewModel.GameId = log.GameId;
                    logViewModel.DateTime = log.DateTime;
                    logViewModel.Message = log.Message;
                    logViewModels.Add(logViewModel);
                }

                return logViewModels;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
    }
}
