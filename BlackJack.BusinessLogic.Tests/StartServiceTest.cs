using BlackJack.BusinessLogic.Services;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.Start;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Tests
{
    [TestFixture]
    public class StartServiceTest
    {
        private StartService _startService;
        private Mock<IGameRepository> _mockGameRepository;
        
        [Test]
        public void SearchGameTestForGameExist()
        {
            string userName = "Mark";
            var game = new Game();

            _mockGameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
            _mockGameRepository.Setup(p => p.GetByHumanName(userName)).Returns(Task.FromResult(game));
            
            _startService = new StartService(_mockGameRepository.Object, null, null, null);
            SearchGameStartView result = _startService.SearchGame(userName).Result;

            Assert.That(result.IsGameExist, Is.EqualTo(true));
        }

        [Test]
        public void SearchGameTestForGameNotExist()
        {
            string userName = "Mark";
            Game game = null;

            _mockGameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
            _mockGameRepository.Setup(p => p.GetByHumanName(userName)).Returns(Task.FromResult(game));

            _startService = new StartService(_mockGameRepository.Object, null, null, null);
            SearchGameStartView result = _startService.SearchGame(userName).Result;

            Assert.That(result.IsGameExist, Is.EqualTo(false));
        }
    }
}


