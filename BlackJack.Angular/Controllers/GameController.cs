using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels.Game;
using NLog;

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

        [Route("DoRoundFirstPhase"), HttpPost]
        public async Task<IHttpActionResult> DoRoundFirstPhase(DoRoundFirstPhaseRequestViewModel doRoundFirstPhaseRequestViewModel)
        {
            try
            {
                string message = await _gameService.ValidateBet(doRoundFirstPhaseRequestViewModel.Bet, doRoundFirstPhaseRequestViewModel.GamePlayerId);

                if (string.IsNullOrEmpty(message))
                {
                    DoRoundFirstPhaseResponseViewModel doRoundFirstPhaseResponseViewModel = await _gameService.DoRoundFirstPhase(doRoundFirstPhaseRequestViewModel.Bet, doRoundFirstPhaseRequestViewModel.GameId);
                    return Ok(new { Message = message, Data = doRoundFirstPhaseResponseViewModel });
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

        [Route("ResumeAfterRoundFirstPhase"), HttpGet]
        public async Task<IHttpActionResult> ResumeAfterRoundFirstPhase(long gameId)
        {
            try
            {
                DoRoundFirstPhaseResponseViewModel doRoundFirstPhaseResponseViewModel = await _gameService.ResumeAfterRoundFirstPhase(gameId);
                return Ok(doRoundFirstPhaseResponseViewModel);
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
                return Ok(addCardViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("DoRoundSecondPhase"), HttpPost]
        public async Task<IHttpActionResult> DoRoundSecondPhase(DoRoundSecondPhaseRequestViewModel doRoundSecondPhaseRequestViewModel)
        {
            try
            {
                DoRoundSecondPhaseResponseViewModel doRoundSecondPhaseResponseViewModel = await _gameService.DoRoundSecondPhase(doRoundSecondPhaseRequestViewModel.GameId, doRoundSecondPhaseRequestViewModel.ContinueBlackJackDanger);
                return Ok(doRoundSecondPhaseResponseViewModel);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("ResumeAfterRoundSecondPhase"), HttpGet]
        public async Task<IHttpActionResult> ResumeAfterRoundSecondPhase(long gameId)
        {
            try
            {
                DoRoundSecondPhaseResponseViewModel doRoundSecondPhaseResponseViewModel = await _gameService.ResumeAfterRoundSecondPhase(gameId);
                return Ok(doRoundSecondPhaseResponseViewModel);
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