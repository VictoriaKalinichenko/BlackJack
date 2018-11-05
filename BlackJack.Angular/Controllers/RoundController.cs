using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.Round;
using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Angular.Controllers
{
    [RoutePrefix("Round")]
    public class RoundController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IRoundService _roundService;

        public RoundController(IRoundService roundService)
        {
            _roundService = roundService;
        }

        [Route("Start"), HttpGet]
        public async Task<IHttpActionResult> Start(long gameId)
        {
            try
            {
                StartRoundView view = await _roundService.Start(gameId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameProcessingError);
            }
        }

        [Route("AddCard"), HttpGet]
        public async Task<IHttpActionResult> AddCard(long gameId)
        {
            try
            {
                AddCardRoundView view = await _roundService.AddCard(gameId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameProcessingError);
            }
        }

        [Route("Continue"), HttpGet]
        public async Task<IHttpActionResult> Continue(long gameId)
        {
            try
            {
                ContinueRoundView view = await _roundService.Continue(gameId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameProcessingError);
            }
        }

        [Route("Restore"), HttpGet]
        public async Task<IHttpActionResult> Restore(long gameId)
        {
            try
            {
                RestoreRoundView view = await _roundService.Restore(gameId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameProcessingError);
            }
        }
    }
}