using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlackJack.BusinessLogic.Services;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.UI.Controllers
{
    public class StartGameController : Controller
    {
        private readonly IStartGameService _startGameService;


        public StartGameController ()
        {
            _startGameService = new StartGameService();
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult StartGame()
        {
            NameAndBotAmountInputViewModel startInputViewModel = new NameAndBotAmountInputViewModel();
            return View(startInputViewModel);
        }
        [HttpPost]
        public ActionResult StartGame(NameAndBotAmountInputViewModel startInputViewModel)
        {
            string validationString = _startGameService.PlayerNameValidation(startInputViewModel.HumanName);

            if (String.IsNullOrEmpty(validationString))
            {
                int gameId = _startGameService.CreateGame(startInputViewModel);
                return RedirectToAction("RoundStart", "Game", new { gameId = gameId });
            }

            ModelState.AddModelError("HumanName", validationString);

            return View(startInputViewModel);
        }
    }
}