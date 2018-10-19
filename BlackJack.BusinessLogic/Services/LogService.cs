﻿using AutoMapper;
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
        private readonly IHistoryMessageRepository _logRepository;
        
        public LogService(IHistoryMessageRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<GetAllLogView>> GetAll()
        {
            IEnumerable<HistoryMessage> logs = await _logRepository.GetAll();
            IEnumerable<GetAllLogView> logViews = Mapper.Map<IEnumerable<HistoryMessage>, IEnumerable<GetAllLogView>>(logs);
            return logViews;
        }
    }
}
