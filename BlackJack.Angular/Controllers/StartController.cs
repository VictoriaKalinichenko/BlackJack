using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels.Start;
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
        private readonly IStartService _startGameService;


        public StartController (IStartService startGameService)
        {
            _startGameService = startGameService;
        }

        [Route("AuthorizePlayer"), HttpGet]
        public async Task<IHttpActionResult> AuthorizePlayer(string userName)
        {
            try
            {
                if (String.IsNullOrEmpty(userName))
                {
                    BadRequest(GameMessageHelper.ReceivedDataError);
                }

                AuthorizePlayerViewModel authorizePlayerViewModel = await _startGameService.AuthorizePlayer(userName);
                return Ok(authorizePlayerViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.PlayerAuthError);
            }
        }

        [Route("CreateGame"), HttpPost]
        public async Task<IHttpActionResult> CreateGame(CreateGameViewModel createGameViewModel)
        {
            try
            {
                if (createGameViewModel == null || createGameViewModel.PlayerId == 0)
                {
                    BadRequest(GameMessageHelper.ReceivedDataError);
                }

                long gameId = await _startGameService.CreateGame(createGameViewModel.PlayerId, createGameViewModel.AmountOfBots);
                return Ok(new { GameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameCreationError);
            }
        }

        [Route("ResumeGame"), HttpGet]
        public async Task<IHttpActionResult> ResumeGame(long playerId)
        {
            try
            {
                if (playerId == 0)
                {
                    BadRequest(GameMessageHelper.ReceivedDataError);
                }

                long gameId = await _startGameService.ResumeGame(playerId);
                return Ok(new { GameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameResumingError);
            }
        }

        [Route("InitRound"), HttpGet]
        public async Task<IHttpActionResult> InitRound(long gameId)
        {
            try
            {
                if (gameId == 0)
                {
                    BadRequest(GameMessageHelper.ReceivedDataError);
                }

                InitRoundViewModel initRoundViewModel = await _startGameService.InitRound(gameId);
                return Ok(initRoundViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameLoadingError);
            }
        }
    }
}