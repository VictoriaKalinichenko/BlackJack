using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels.Game;
using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Angular.Controllers
{
    [RoutePrefix("Game")]
    public class GameController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("EndGame"), HttpPost]
        public async Task<IHttpActionResult> EndGame(EndGameViewModel endGameViewModel)
        {
            try
            {
                if (endGameViewModel == null || endGameViewModel.GameId == 0 || endGameViewModel.Result == null)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

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
                if (startRoundRequestViewModel == null || startRoundRequestViewModel.GameId == 0 ||
                    startRoundRequestViewModel.Bet == 0 || startRoundRequestViewModel.GamePlayerId == 0)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

                string message = await _gameService.ValidateBet(startRoundRequestViewModel.Bet, startRoundRequestViewModel.GamePlayerId);

                if (string.IsNullOrEmpty(message))
                {
                    StartRoundResponseViewModel startRoundResponseViewModel = await _gameService.StartRound(startRoundRequestViewModel);
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
                if (gameId == 0)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

                StartRoundResponseViewModel startRoundResponseViewModel = await _gameService.ResumeAfterStartRound(gameId);
                return Ok(startRoundResponseViewModel);
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
                if (gameId == 0)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

                AddCardViewModel addCardViewModel = await _gameService.AddCard(gameId);
                return Ok(addCardViewModel);
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
                if (continueRoundRequestViewModel == null || continueRoundRequestViewModel.GameId == 0)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

                ContinueRoundResponseViewModel continueRoundResponseViewModel = await _gameService.ContinueRound(continueRoundRequestViewModel);
                return Ok(continueRoundResponseViewModel);
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
                if (gameId == 0)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

                ContinueRoundResponseViewModel continueRoundResponseViewModel = 
                    await _gameService.ResumeAfterContinueRound(gameId);
                return Ok(continueRoundResponseViewModel);
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
                if (gameId == 0)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

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