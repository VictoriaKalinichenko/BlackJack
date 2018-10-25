using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels;
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

        [Route("AuthorizePlayer"), HttpGet]
        public async Task<IHttpActionResult> AuthorizePlayer(string userName)
        {
            try
            {
                if (String.IsNullOrEmpty(userName))
                {
                    BadRequest(GameMessage.ReceivedDataError);
                }

                AuthorizePlayerStartView authorizePlayerStartView = await _startService.AuthorizePlayer(userName);
                return Ok(authorizePlayerStartView);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessage.PlayerAuthError);
            }
        }

        [Route("CreateGame"), HttpPost]
        public async Task<IHttpActionResult> CreateGame(CreateGameStartView createGameStartView)
        {
            try
            {
                if (createGameStartView == null)
                {
                    BadRequest(GameMessage.ReceivedDataError);
                }

                long gameId = await _startService.CreateGame(createGameStartView.PlayerId, createGameStartView.AmountOfBots);
                return Ok(new { GameId = gameId });
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessage.GameCreationError);
            }
        }

        [Route("ResumeGame"), HttpGet]
        public async Task<IHttpActionResult> ResumeGame(long playerId)
        {
            try
            {
                long gameId = await _startService.ResumeGame(playerId);
                return Ok(new { GameId = gameId });
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessage.GameResumingError);
            }
        }

        [Route("Initialize"), HttpGet]
        public async Task<IHttpActionResult> Initialize(long gameId)
        {
            try
            {
                InitializeStartView initializeStartView = await _startService.InitializeRound(gameId);
                return Ok(initializeStartView);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessage.GameLoadingError);
            }
        }
    }
}