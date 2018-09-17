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


        public StartGameController(IStartGameService startGameService)
        {
            _startGameService = startGameService;
        }
        
        [Route("GetAuthorizedPlayer"), HttpPost]
        public async Task<IHttpActionResult> AuthorizedPlayer(UserNameViewModel userNameViewModel)
        {
            try
            {
                string userName = await _startGameService.PlayerCreation(userNameViewModel.UserName);
                AuthorizedPlayerViewModel authorizedPlayerViewModel = await _startGameService.PlayerAuthorization(userName);
                return Ok(authorizedPlayerViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.PlayerAuthError);
            }
        }

        [Route("CreateNewGame"), HttpPost]
        public async Task<IHttpActionResult> CreateNewGame(GameCreationViewModel gameCreationViewModel)
        {
            try
            {
                int gameId = await _startGameService.CreateGame(gameCreationViewModel.PlayerId, gameCreationViewModel.AmountOfBots);
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

        [Route("GetGame"), HttpGet]
        public async Task<IHttpActionResult> GetGame(int gameId)
        {
            try
            {
                RoundViewModel game = await _startGameService.GetGame(gameId);
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