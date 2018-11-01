using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.Round;
using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.UI.Controllers
{
    [RoutePrefix("Round")]
    public class RoundController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IRoundService _roundService;

        public RoundController (IRoundService roundService)
        {
            _roundService = roundService;
        }
        
        [Route("Start"), HttpGet]
        public async Task<IHttpActionResult> Start(long gameId)
        {
            try
            {
                StartRoundView startRoundView = await _roundService.Start(gameId);
                return Ok(startRoundView);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameError);
            }
        }
        
        [Route("AddCard"), HttpGet]
        public async Task<IHttpActionResult> AddCard(long gameId)
        {
            try
            {
                AddCardRoundView addCardRoundView = await _roundService.AddCard(gameId);
                return Ok(addCardRoundView);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameError);
            }
        }
        
        [Route("Continue"), HttpGet]
        public async Task<IHttpActionResult> Continue(long gameId)
        {
            try
            {
                ContinueRoundView continueRoundView = await _roundService.Continue(gameId);
                return Ok(continueRoundView);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameError);
            }
        }

        [Route("Restore"), HttpGet]
        public async Task<IHttpActionResult> Restore(long gameId)
        {
            try
            {
                RestoreRoundView restoreRoundView = await _roundService.Restore(gameId);
                return Ok(restoreRoundView);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameError);
            }
        }
    }
}