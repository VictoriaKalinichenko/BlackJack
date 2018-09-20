using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.Angular.Controllers
{
    [RoutePrefix("StartGame")]
    public class StartGameController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IStartGameService _startGameService;


        public StartGameController (IStartGameService startGameService)
        {
            _startGameService = startGameService;
        }

        [Route("AuthorizePlayer"), HttpGet]
        public async Task<IHttpActionResult> AuthorizePlayer(string userName)
        {
            try
            {
                StartGameAuthorizePlayerView startGameAuthorizePlayerView = await _startGameService.AuthorizePlayer(userName);
                return Ok(startGameAuthorizePlayerView);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.PlayerAuthError);
            }
        }

        [Route("CreateNewGame"), HttpPost]
        public async Task<IHttpActionResult> CreateNewGame(AngularStartGameCreateNewGameView requestView)
        {
            try
            {
                int gameId = await _startGameService.CreateGame(requestView.PlayerId, requestView.AmountOfBots);
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
        public async Task<IHttpActionResult> ResumeGame(int playerId)
        {
            try
            {
                int gameId = await _startGameService.ResumeGame(playerId);
                return Ok(new { GameId = gameId });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameResumingError);
            }
        }

        [Route("StartRound"), HttpGet]
        public async Task<IHttpActionResult> StartRound(int gameId)
        {
            try
            {
                StartGameStartRoundView game = await _startGameService.GetStartGameStartRoundView(gameId);
                return Ok(game);
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