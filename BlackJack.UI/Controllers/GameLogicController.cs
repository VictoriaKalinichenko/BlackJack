using System;
using System.Web.Http;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using NLog;

namespace BlackJack.UI.Controllers
{
    public class GameLogicController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGameLogicService _gameLogicService;

        public GameLogicController (IGameLogicService gameLogicService)
        {
            _gameLogicService = gameLogicService;
        }

        [Route("GameLogic/RoundStart"), HttpGet]
        public async Task<IHttpActionResult> RoundStart(int inGameId)
        {
            try
            {
                await _gameLogicService.RoundFirstPhase(inGameId);
                string message = GameMessageHelper.Success;
                return Json(new { Message = message });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
        
        [Route("GameLogic/FirstPhaseGamePlay"), HttpGet]
        public async Task<IHttpActionResult> FirstPhaseGamePlay(int inGameId)
        {
            try
            {
                bool humanBjAndDealerBjDanger = await _gameLogicService.IsHumanBjAndDealerBjDanger(inGameId);
                bool canHumanTakeOneMoreCard = await _gameLogicService.CanHumanTakeOneMoreCard(inGameId);
                string message = GameMessageHelper.Success;
                return Json(new { Message = message, HumanBjAndDealerBjDanger = humanBjAndDealerBjDanger, CanHumanTakeOneMoreCard = canHumanTakeOneMoreCard });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
        
        [Route("GameLogic/HumanBjAndDealerBjDangerContinueRound"), HttpGet]
        public async Task<IHttpActionResult> HumanBjAndDealerBjDangerContinueRound(int inGameId)
        {
            try
            {
                await _gameLogicService.HumanBjAndDealerBjDangerContinueRound(inGameId);
                string message = GameMessageHelper.Success;
                return Json(new { Message = message });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
        
        [Route("GameLogic/AddOneMoreCardToHuman"), HttpGet]
        public async Task<IHttpActionResult> AddOneMoreCardToHuman(int inGameId)
        {
            try
            {
                await _gameLogicService.AddOneMoreCardToHuman(inGameId);
                bool canHumanTakeOneMoreCard = await _gameLogicService.CanHumanTakeOneMoreCard(inGameId);
                string message = GameMessageHelper.Success;
                return Json(new { Message = message, CanHumanTakeOneMoreCard = canHumanTakeOneMoreCard });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
        
        [Route("GameLogic/SecondPhase"), HttpGet]
        public async Task<IHttpActionResult> SecondPhase(int inGameId)
        {
            try
            {
                await _gameLogicService.RoundSecondPhase(inGameId);
                string message = GameMessageHelper.Success;
                return Json(new { Message = message });
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                message = GameMessageHelper.GameError;
                return Json(new { Message = message });
            }
        }
    }
}