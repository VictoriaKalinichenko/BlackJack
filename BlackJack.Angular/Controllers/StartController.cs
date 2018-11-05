﻿using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Services.Interfaces;
using BlackJack.ViewModels.Start;
using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Angular.Controllers
{
    [RoutePrefix("Start")]
    public class StartController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IStartService _startService;
        
        public StartController (IStartService startService)
        {
            _startService = startService;
        }

        [Route("Index"), HttpGet]
        public async Task<IHttpActionResult> Index(string userName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(userName))
                {
                    new Exception(GameMessage.ReceivedDataError);
                }

                IndexStartView view = await _startService.SearchGameForPlayer(userName);
                return Ok(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.PlayerAuthorizationError);
            }
        }

        [Route("CreateGame"), HttpPost]
        public async Task<IHttpActionResult> CreateGame(CreateGameStartView view)
        {
            try
            {
                if (view == null ||
                    String.IsNullOrWhiteSpace(view.UserName))
                {
                    new Exception(GameMessage.ReceivedDataError);
                }

                long gameId = await _startService.CreateGame(view);
                return Ok(gameId);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameCreationError);
            }
        }
        
        [Route("Initialize"), HttpGet]
        public async Task<IHttpActionResult> Initialize(long gameId)
        {
            try
            {
                InitializeStartView view = await _startService.InitializeRound(gameId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameLoadingError);
            }
        }
    }
}