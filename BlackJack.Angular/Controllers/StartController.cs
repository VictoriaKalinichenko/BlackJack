using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels.Start;
using NLog;

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