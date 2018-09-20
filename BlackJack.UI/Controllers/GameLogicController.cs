using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.UI.Controllers
{
    [RoutePrefix("GameLogic")]
    public class GameLogicController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGameLogicService _gameLogicService;

        public GameLogicController (IGameLogicService gameLogicService)
        {
            _gameLogicService = gameLogicService;
        }

        [Route("EndGame"), HttpPost]
        public async Task<IHttpActionResult> EndGame(GameLogicEndGameView gameLogicEndGameView)
        {
            try
            {
                await _gameLogicService.EndGame(gameLogicEndGameView);
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
        public async Task<IHttpActionResult> DoRoundFirstPhase(GameLogicDoRoundFirstPhaseRequestView gameLogicDoRoundFirstPhaseRequestView)
        {
            try
            {
                string message = await _gameLogicService.ValidateBet(gameLogicDoRoundFirstPhaseRequestView.Bet, gameLogicDoRoundFirstPhaseRequestView.GamePlayerId);

                if (string.IsNullOrEmpty(message))
                {
                    GameLogicDoRoundFirstPhaseResponseView gameLogicDoRoundFirstPhaseResponseView = await _gameLogicService.DoRoundFirstPhase(gameLogicDoRoundFirstPhaseRequestView.Bet, gameLogicDoRoundFirstPhaseRequestView.GameId);
                    return Ok(new { Message = message, Data = gameLogicDoRoundFirstPhaseResponseView });
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

        [Route("ResumeGameAfterRoundFirstPhase"), HttpGet]
        public async Task<IHttpActionResult> ResumeGameAfterRoundFirstPhase(int gameId)
        {
            try
            {
                GameLogicResumeGameAfterRoundFirstPhaseView gameLogicResumeGameAfterRoundFirstPhaseView = await _gameLogicService.ResumeGameAfterRoundFirstPhase(gameId);
                return Ok(new { Data = gameLogicResumeGameAfterRoundFirstPhaseView });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }
        
        [Route("AddOneMoreCardToHuman"), HttpGet]
        public async Task<IHttpActionResult> AddOneMoreCardToHuman(int gameId)
        {
            try
            {
                GameLogicAddOneMoreCardToHumanView gameLogicAddOneMoreCardToHumanView = await _gameLogicService.AddOneMoreCardToHuman(gameId);
                return Ok(new { Data = gameLogicAddOneMoreCardToHumanView });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }
        
        [Route("DoRoundSecondPhase"), HttpPost]
        public async Task<IHttpActionResult> DoRoundSecondPhase(GameLogicDoRoundSecondPhaseRequestView gameLogicDoRoundSecondPhaseRequestView)
        {
            try
            {
                GameLogicDoRoundSecondPhaseResponseView gameLogicDoRoundSecondPhaseResponseView = await _gameLogicService.DoRoundSecondPhase(gameLogicDoRoundSecondPhaseRequestView.GameId, gameLogicDoRoundSecondPhaseRequestView.HumanBlackJackAndDealerBlackJackDanger);
                return Ok(new { Data = gameLogicDoRoundSecondPhaseResponseView });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("ResumeGameAfterRoundSecondPhase"), HttpGet]
        public async Task<IHttpActionResult> ResumeGameAfterRoundSecondPhase(int gameId)
        {
            try
            {
                GameLogicResumeGameAfterRoundSecondPhaseView gameLogicResumeGameAfterRoundSecondPhaseView = await _gameLogicService.ResumeGameAfterRoundSecondPhase(gameId);
                return Ok(new { Data = gameLogicResumeGameAfterRoundSecondPhaseView });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("EndRound"), HttpGet]
        public async Task<IHttpActionResult> EndRound(int gameId)
        {
            try
            {
                await _gameLogicService.EndRound(gameId);
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