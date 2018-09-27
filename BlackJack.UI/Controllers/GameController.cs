using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels.Game;
using NLog;

namespace BlackJack.UI.Controllers
{
    [RoutePrefix("Game")]
    public class GameController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGameService _gameService;

        public GameController (IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("EndGame"), HttpPost]
        public async Task<IHttpActionResult> EndGame(EndGameViewModel endGameViewModel)
        {
            try
            {
                await _gameService.EndGame(endGameViewModel);
                return Ok(GameMessageHelper.Success);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("StartRound"), HttpPost]
        public async Task<IHttpActionResult> StartRound(StartRoundRequestViewModel startRoundRequestViewModel)
        {
            try
            {
                string message = await _gameService.ValidateBet(startRoundRequestViewModel.Bet, startRoundRequestViewModel.GamePlayerId);

                if (string.IsNullOrEmpty(message))
                {
                    StartRoundResponseViewModel startRoundResponseViewModel = await _gameService.StartRound(startRoundRequestViewModel.Bet, startRoundRequestViewModel.GameId);
                    return Ok(new { Message = message, Data = startRoundResponseViewModel });
                }

                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("ResumeAfterStartRound"), HttpGet]
        public async Task<IHttpActionResult> ResumeAfterStartRound(long gameId)
        {
            try
            {
                StartRoundResponseViewModel startRoundResponseViewModel = await _gameService.ResumeAfterStartRound(gameId);
                return Ok(new { Data = startRoundResponseViewModel });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }
        
        [Route("AddCard"), HttpGet]
        public async Task<IHttpActionResult> AddCard(long gameId)
        {
            try
            {
                AddCardViewModel addCardViewModel = await _gameService.AddCard(gameId);
                return Ok(new { Data = addCardViewModel });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }
        
        [Route("ContinueRound"), HttpPost]
        public async Task<IHttpActionResult> ContinueRound(ContinueRoundRequestViewModel continueRoundRequestViewModel)
        {
            try
            {
                ContinueRoundResponseViewModel continueRoundResponseViewModel = await _gameService.ContinueRound(continueRoundRequestViewModel.GameId, continueRoundRequestViewModel.BlackJackContinueChoice);
                return Ok(new { Data = continueRoundResponseViewModel });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("ResumeAfterContinueRound"), HttpGet]
        public async Task<IHttpActionResult> ResumeAfterContinueRound(long gameId)
        {
            try
            {
                ContinueRoundResponseViewModel continueRoundResponseViewModel = await _gameService.ResumeAfterContinueRound(gameId);
                return Ok(new { Data = continueRoundResponseViewModel });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("EndRound"), HttpGet]
        public async Task<IHttpActionResult> EndRound(long gameId)
        {
            try
            {
                await _gameService.EndRound(gameId);
                return Ok(GameMessageHelper.Success);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }
    }
}