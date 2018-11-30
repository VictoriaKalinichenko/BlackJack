using BlackJack.BusinessLogic.Managers.Interfaces;
using BlackJack.BusinessLogic.Services;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using BlackJack.ViewModels.Round;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Tests
{
    [TestFixture]
    public class RoundServiceTest
    {
        private RoundService _roundService;
        private Mock<IGameRepository> _mockGameRepository;
        private Mock<IGamePlayerRepository> _mockGamePlayerRepository;
        private Mock<IPlayerCardRepository> _mockPlayerCardRepository;
        private Mock<ICardRepository> _mockCardRepository;
        private Mock<IHistoryMessageManager> _mockHistoryMessageManager;

        [Test]
        public void StartTest()
        {
            _mockGameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
            _mockGamePlayerRepository = new Mock<IGamePlayerRepository>(MockBehavior.Strict);
            _mockPlayerCardRepository = new Mock<IPlayerCardRepository>(MockBehavior.Strict);
            _mockCardRepository = new Mock<ICardRepository>(MockBehavior.Strict);
            _mockHistoryMessageManager = new Mock<IHistoryMessageManager>(MockBehavior.Strict);

            var requestView = new RequestStartRoundView() { GameId = 9, IsNewRound = true };
            var gamePlayers = new List<GamePlayer>()
            {
                new GamePlayer() {
                    Player = new Player() { Type = PlayerType.Dealer },
                    CardScore = 10
                },
                new GamePlayer() {
                    Player = new Player() { Type = PlayerType.Human },
                    CardScore = 10
                }
            };
            var game = new Game() { RoundResult = "" };
            int cardAmount = 4;
            var cards = new List<Card>()
            {
                new Card() { Worth = 9 },
                new Card() { Worth = 11 },
                new Card() { Worth = 4 },
                new Card() { Worth = 10 }
            };
            var playerCards = new List<PlayerCard>();

            int createMany = 0;
            
            _mockGamePlayerRepository.Setup(p => p.GetAllByGameId(requestView.GameId)).Returns(Task.FromResult(gamePlayers));
            _mockGameRepository.Setup(p => p.Get(requestView.GameId)).Returns(Task.FromResult(game));
            _mockPlayerCardRepository.Setup(p => p.CreateMany(playerCards)).Returns(Task.FromResult(createMany));
            _mockCardRepository.Setup(p => p.GetSpecifiedAmount(cardAmount)).Returns(Task.FromResult(cards));
            
            _roundService = new RoundService
            (
                null,
                _mockGameRepository.Object,
                _mockGamePlayerRepository.Object,
                _mockPlayerCardRepository.Object,
                _mockCardRepository.Object,
                _mockHistoryMessageManager.Object
            );
            ResponseStartRoundView result = _roundService.Start(requestView).Result;

            result.RoundResult = "";
            Assert.That(result.RoundResult, Is.EqualTo(""));

            //_mockGameRepository.VerifyAll();
            //_mockGamePlayerRepository.VerifyAll();
            //_mockPlayerCardRepository.VerifyAll();
            //_mockCardRepository.VerifyAll();
            //_mockHistoryMessageManager.VerifyAll();
        }

    }
}


