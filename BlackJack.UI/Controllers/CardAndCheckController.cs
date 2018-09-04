using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using NLog;

namespace BlackJack.UI.Controllers
{
    public class CardAndCheckController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ICardAndCheckService _cardAndCheckService;

        public CardAndCheckController (ICardAndCheckService cardAndCheckService)
        {
            _cardAndCheckService = cardAndCheckService;
        }

        public ActionResult Error(string message)
        {
            return View((object)message);
        }
        
        [HttpPost]
        public async Task<ActionResult> RoundStart(int inGameId)
        {
            try
            {
                await _cardAndCheckService.RoundFirstPhase(inGameId);
                string message = GameMessage.Success;
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> FirstPhaseGamePlay(int inGameId)
        {
            try
            {
                bool humanBjAndDealerBjDanger = await _cardAndCheckService.IsHumanBjAndDealerBjDanger(inGameId);
                bool canHumanTakeOneMoreCard = await _cardAndCheckService.CanHumanTakeOneMoreCard(inGameId);
                string message = GameMessage.Success;
                return Json(new { Message = message, HumanBjAndDealerBjDanger = humanBjAndDealerBjDanger, CanHumanTakeOneMoreCard = canHumanTakeOneMoreCard }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> HumanBjAndDealerBjDangerContinueRound(int inGameId)
        {
            try
            {
                await _cardAndCheckService.HumanBjAndDealerBjDangerContinueRound(inGameId);
                string message = GameMessage.Success;
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOneMoreCardToHuman(int inGameId)
        {
            try
            {
                await _cardAndCheckService.AddOneMoreCardToHuman(inGameId);
                bool canHumanTakeOneMoreCard = await _cardAndCheckService.CanHumanTakeOneMoreCard(inGameId);
                string message = GameMessage.Success;
                return Json(new { Message = message, CanHumanTakeOneMoreCard = canHumanTakeOneMoreCard }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SecondPhase(int inGameId)
        {
            try
            {
                await _cardAndCheckService.RoundSecondPhase(inGameId);
                string message = GameMessage.Success;
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}