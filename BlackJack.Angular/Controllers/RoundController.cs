using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels;
using BlackJack.ViewModels.ViewModels.Game;
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

        public RoundController(IRoundService gameService)
        {
            _roundService = gameService;
        }

        [Route("EndGame"), HttpPost]
        public async Task<IHttpActionResult> EndGame(EndGameRoundView endGameRoundView)
        {
            try
            {
                if (endGameRoundView == null || endGameRoundView.Result == null)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

                await _roundService.EndGame(endGameRoundView);
                return Ok(GameMessageHelper.Success);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("Start"), HttpPost]
        public async Task<IHttpActionResult> Start(RequestStartRoundView requestStartRoundView)
        {
            try
            {
                if (requestStartRoundView == null)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

                ResponseStartRoundView responseStartRoundView = await _roundService.Start(requestStartRoundView);
                if (responseStartRoundView == null)
                {
                    return Ok(new { Message = GameMessageHelper.BetIsNotValid });
                }

                return Ok(new { Data = responseStartRoundView });
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("ResumeAfterStart"), HttpGet]
        public async Task<IHttpActionResult> ResumeAfterStart(long gameId)
        {
            try
            {
                ResumeAfterStartRoundView resumeAfterStartRoundView = await _roundService.ResumeAfterStart(gameId);
                return Ok(resumeAfterStartRoundView);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
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
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("Continue"), HttpPost]
        public async Task<IHttpActionResult> Continue(RequestContinueRoundView requestContinueRoundView)
        {
            try
            {
                if (requestContinueRoundView == null)
                {
                    return BadRequest(GameMessageHelper.ReceivedDataError);
                }

                ResponseContinueRoundView responseContinueRoundView = await _roundService.Continue(requestContinueRoundView);
                return Ok(responseContinueRoundView);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("ResumeAfterContinue"), HttpGet]
        public async Task<IHttpActionResult> ResumeAfterContinue(long gameId)
        {
            try
            {
                ResumeAfterContinueRoundView resumeAfterContinueRoundView = await _roundService.ResumeAfterContinue(gameId);
                return Ok(resumeAfterContinueRoundView);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }

        [Route("End"), HttpGet]
        public async Task<IHttpActionResult> End(long gameId)
        {
            try
            {
                await _roundService.EndRound(gameId);
                return Ok(GameMessageHelper.Success);
            }
            catch (Exception exception)
            {
                string message = LogHelper.ToString(exception);
                _logger.Error(message);
                return BadRequest(GameMessageHelper.GameError);
            }
        }
    }
}