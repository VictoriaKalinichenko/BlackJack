using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels.Start;
using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Angular.Controllers
{
    [RoutePrefix("Start")]
    public class StartController : ApiController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IStartService _startService;
        
        public StartController (IStartService startService)
        {
            _startService = startService;
        }

        [Route("Index"), HttpGet]
        public async Task<IHttpActionResult> Index(string userName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(userName))
                {
                    new Exception(GameMessage.ReceivedDataError);
                }

                IndexStartView indexStartView = await _startService.SearchGameForPlayer(userName);
                return Ok(indexStartView);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.PlayerAuthorizationError);
            }
        }

        [Route("CreateGame"), HttpPost]
        public async Task<IHttpActionResult> CreateGame(CreateGameStartView createGameStartView)
        {
            try
            {
                if (createGameStartView == null ||
                    String.IsNullOrWhiteSpace(createGameStartView.UserName))
                {
                    new Exception(GameMessage.ReceivedDataError);
                }

                long gameId = await _startService.CreateGame(createGameStartView);
                return Ok(gameId);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameCreationError);
            }
        }
        
        [Route("Initialize"), HttpGet]
        public async Task<IHttpActionResult> Initialize(long gameId)
        {
            try
            {
                InitializeStartView initializeStartView = await _startService.InitializeRound(gameId);
                return Ok(initializeStartView);
            }
            catch (Exception exception)
            {
                string message = exception.ToString();
                _logger.Error(message);
                return BadRequest(GameMessage.GameLoadingError);
            }
        }
    }
}