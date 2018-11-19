using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Services.Interfaces;
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
        
        [Route("Start"), HttpPost]
        public async Task<IHttpActionResult> Start(RequestStartRoundView requestView)
        {
            try
            {
                ResponseStartRoundView responseView = await _roundService.Start(requestView);
                return Ok(responseView);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameProcessingError);
            }
        }
        
        [Route("TakeCard"), HttpGet]
        public async Task<IHttpActionResult> TakeCard(long gameId)
        {
            try
            {
                TakeCardRoundView view = await _roundService.TakeCard(gameId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameProcessingError);
            }
        }
        
        [Route("End"), HttpGet]
        public async Task<IHttpActionResult> End(long gameId)
        {
            try
            {
                EndRoundView view = await _roundService.End(gameId);
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