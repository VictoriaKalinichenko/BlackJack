using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels.GameLogic;
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
                    GameLogicRoundFirstPhaseResponseView gameLogicResponseView = await _gameLogicService.DoRoundFirstPhase(gameLogicDoRoundFirstPhaseRequestView.Bet, gameLogicDoRoundFirstPhaseRequestView.GameId);
                    return Ok(new { Message = message, Data = gameLogicResponseView });
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
        public async Task<IHttpActionResult> ResumeGameAfterRoundFirstPhase(long gameId)
        {
            try
            {
                GameLogicRoundFirstPhaseResponseView gameLogicResponseView = await _gameLogicService.ResumeGameAfterRoundFirstPhase(gameId);
                return Ok(new { Data = gameLogicResponseView });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }
        
        [Route("AddCardToHuman"), HttpGet]
        public async Task<IHttpActionResult> AddCardToHuman(long gameId)
        {
            try
            {
                GameLogicAddCardToHumanView gameLogicAddCardToHumanView = await _gameLogicService.AddCardToHuman(gameId);
                return Ok(new { Data = gameLogicAddCardToHumanView });
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
                GameLogicRoundSecondPhaseResponseView gameLogicResponseView = await _gameLogicService.DoRoundSecondPhase(gameLogicDoRoundSecondPhaseRequestView.GameId, gameLogicDoRoundSecondPhaseRequestView.HumanBlackJackAndDealerBlackJackDanger);
                return Ok(new { Data = gameLogicResponseView });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("ResumeGameAfterRoundSecondPhase"), HttpGet]
        public async Task<IHttpActionResult> ResumeGameAfterRoundSecondPhase(long gameId)
        {
            try
            {
                GameLogicRoundSecondPhaseResponseView gameLogicResponseView = await _gameLogicService.ResumeGameAfterRoundSecondPhase(gameId);
                return Ok(new { Data = gameLogicResponseView });
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