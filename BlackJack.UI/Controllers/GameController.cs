using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlackJack.BusinessLogic.Services;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.UI.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController ()
        {
            _gameService = new GameService();
        }

        public ActionResult RoundStart(int gameId)
        {
            BetInputViewModel betInputViewModel = new BetInputViewModel();
            betInputViewModel.Game = _gameService.GenerateGameStartViewModel(gameId);
            betInputViewModel.HumanBet = BetValue._betGenCoef;
            return View(betInputViewModel);
        }
        [HttpPost]
        public ActionResult RoundStart(BetInputViewModel betInputViewModel)
        {
            if (ModelState.IsValid)
            {
                int gameId = _gameService.BetsCreation(betInputViewModel);
                return RedirectToAction("CardDistribution", new { gameId = gameId });
            }

            return View(betInputViewModel);
        }

        public ActionResult CardDistribution(int gameId)
        {
            GameViewModel gameViewModel = _gameService.RoundFirstPhase(gameId);
            return View(gameViewModel);
        }
    }
}