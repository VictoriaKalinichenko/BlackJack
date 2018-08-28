using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.ViewModels.ViewModels;
using NLog;

namespace BlackJack.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IPlayerCardRepository _playerCardRepository;

        private readonly IGamePlayerProvider _gamePlayerProvider;
        private readonly IPlayerCardProvider _playerCardProvider;
        private readonly ICardCheckProvider _cardCheckProvider;
        

        public GameService(IPlayerRepository playerRepository, IGameRepository gameRepository, IGamePlayerRepository gamePlayerRepository,
            IPlayerCardRepository playerCardRepository, IGamePlayerProvider gamePlayerProvider, IPlayerCardProvider playerCardProvider, ICardCheckProvider cardCheckProvider)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;

            _gamePlayerProvider = gamePlayerProvider;
            _playerCardProvider = playerCardProvider;
            _cardCheckProvider = cardCheckProvider;
        }
        
        public async Task<int> BetsCreation(BetInputViewModel betInputViewModel)
        {
            try
            {
                int gameId = 0;

                Game game = await _gameRepository.Get(betInputViewModel.Game.Id);
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(game.Id);
                await _gamePlayerProvider.BetsCreation(gamePlayers, betInputViewModel.HumanBet);

                game.Stage = Stage.BetsCreation;
                await _gameRepository.Update(game);

                gameId = game.Id;

                _logger.Info(Environment.StackTrace + "|" + InfoMessage.BetsAreCreated);

                return gameId;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<bool> RoundFirstPhase(int gameId)
        {
            try
            {
                bool humanBjAndDealerBjDanger = false;

                Game game = await _gameRepository.Get(gameId);
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                List<int> deck = CreateDeck();

                await FirstCardsDistribution(gamePlayers, deck);
                _logger.Info(Environment.StackTrace + "|" + InfoMessage.FirstCardsDistributionIsDone);

                humanBjAndDealerBjDanger = await FirstCardCheck(gamePlayers);
                _logger.Info(Environment.StackTrace + "|" + InfoMessage.FirstCardsCheckIsDone);

                game.Stage = Stage.FirstCardsDistribution;
                await _gameRepository.Update(game);

                return humanBjAndDealerBjDanger;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task AddOneMoreCardToHuman(int gameId)
        {
            try
            {
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                GamePlayer human = gamePlayers.Where(g => g.Player.IsHuman).FirstOrDefault();
                List<int> deck = await ResumeDeck(gamePlayers);

                await AddingCardToPlayer(human, deck);
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<bool> CanHumanTakeOneMoreCard(int gameId)
        {
            try
            {
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                GamePlayer human = gamePlayers.Where(g => g.Player.IsHuman).FirstOrDefault();

                bool canHumanTakeOneMoreCard = false;
                canHumanTakeOneMoreCard = _cardCheckProvider.HumanPlayerHasEnoughCards(human.RoundScore);                
                return canHumanTakeOneMoreCard;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task RoundSecondPhase(int gameId)
        {

        }

        public async Task<GameStartViewModel> GenerateGameStartViewModel(int gameId)
        {
            try
            {
                GameStartViewModel gameStartViewModel = new GameStartViewModel();
                gameStartViewModel.Bots = new List<GamePlayerStartViewModel>();
                gameStartViewModel.Id = gameId;

                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    PlayerViewModel playerViewModel = new PlayerViewModel();

                    playerViewModel.Id = gamePlayer.Player.Id;
                    playerViewModel.Name = gamePlayer.Player.Name;

                    if (gamePlayer.Player.IsDealer)
                    {
                        playerViewModel.PlayerType = PlayerType.Dealer;
                        gameStartViewModel.Dealer = new GamePlayerStartViewModel();
                        gameStartViewModel.Dealer.Player = playerViewModel;
                        gameStartViewModel.Dealer.Score = gamePlayer.Score;
                    }

                    if (gamePlayer.Player.IsHuman)
                    {
                        playerViewModel.PlayerType = PlayerType.Human;
                        gameStartViewModel.Human = new GamePlayerStartViewModel();
                        gameStartViewModel.Human.Player = playerViewModel;
                        gameStartViewModel.Human.Score = gamePlayer.Score;
                    }

                    if (!gamePlayer.Player.IsHuman && !gamePlayer.Player.IsDealer)
                    {
                        playerViewModel.PlayerType = PlayerType.Bot;
                        gameStartViewModel.Bots.Add(new GamePlayerStartViewModel() { Player = playerViewModel, Score = gamePlayer.Score });
                    }
                }

                return gameStartViewModel;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        public async Task<GameViewModel> GenerateFirstPhaseGameViewModel(int gameId)
        {
            GameViewModel gameViewModel = new GameViewModel();

            try
            {
                gameViewModel.Id = gameId;
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    GamePlayerViewModel gamePlayerViewModel = new GamePlayerViewModel { Score = gamePlayer.Score };
                    gamePlayerViewModel.Cards = new List<string>();
                    PlayerViewModel playerViewModel = new PlayerViewModel() { Id = gamePlayer.Player.Id, Name = gamePlayer.Player.Name };
                    List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();

                    if (gamePlayer.Player.IsDealer)
                    {
                        playerViewModel.PlayerType = PlayerType.Dealer;
                        gamePlayerViewModel.Player = playerViewModel;
                        gamePlayerViewModel.Cards.Add(_playerCardProvider.PlayerCardToCardString(playerCards[0]));

                        gameViewModel.Dealer = gamePlayerViewModel;
                    }

                    if (!gamePlayer.Player.IsDealer)
                    {
                        gamePlayerViewModel.CardScore = gamePlayer.RoundScore;
                        gamePlayerViewModel.Bet = gamePlayer.Bet;
                        gamePlayerViewModel.Cards = _playerCardProvider.GetCardsStringList(playerCards);
                    }

                    if (gamePlayer.Player.IsHuman)
                    {
                        playerViewModel.PlayerType = PlayerType.Human;
                        gamePlayerViewModel.Player = playerViewModel;

                        gameViewModel.Human = gamePlayerViewModel;
                    }

                    if (!gamePlayer.Player.IsHuman && !gamePlayer.Player.IsDealer)
                    {
                        playerViewModel.PlayerType = PlayerType.Bot;
                        gamePlayerViewModel.Player = playerViewModel;

                        gameViewModel.Bots.Add(gamePlayerViewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
            }

            return gameViewModel;
        }

        private async Task FirstCardsDistribution(IEnumerable<GamePlayer> players, List<int> deck)
        {
            try
            {
                foreach (GamePlayer gamePlayer in players)
                {
                    await AddingCardToPlayer(gamePlayer, deck);
                    await AddingCardToPlayer(gamePlayer, deck);
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private async Task<bool> FirstCardCheck(IEnumerable<GamePlayer> gamePlayers)
        {
            try
            {
                bool humanBjAndDealerBjDanger = false;

                GamePlayer dealer = gamePlayers.Where(m => m.Player.IsDealer).First();
                List<PlayerCard> dealerPlayerCards = (await _playerCardRepository.GetByGamePlayerId(dealer.Id)).ToList();
                Card dealerFirstCard = InitialDeck.Cards.Where(m => m.Id == dealerPlayerCards[0].CardId).First();

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    if (!gamePlayer.Player.IsDealer)
                    {
                        List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
                        gamePlayer.BetPayCoefficient = _cardCheckProvider.RoundFirstPhaseResult(gamePlayer.RoundScore, playerCards.Count, (int)dealerFirstCard.CardName);
                        await _gamePlayerRepository.Update(gamePlayer);
                    }
                }

                GamePlayer human = gamePlayers.Where(m => m.Player.IsHuman).First();
                if (human.BetPayCoefficient == BetValue.BetWinCoefficient)
                {
                    humanBjAndDealerBjDanger = true;
                }

                return humanBjAndDealerBjDanger;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private async Task SecondCardsDistribution(IEnumerable<GamePlayer> players, List<int> deck)
        {
            try
            {
                foreach (GamePlayer gamePlayer in players)
                {
                    await AddingCardToPlayer(gamePlayer, deck);
                    await AddingCardToPlayer(gamePlayer, deck);
                }
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private async Task AddingCardToPlayer(GamePlayer gamePlayer, List<int> deck)
        {
            try
            {
                await _playerCardProvider.AddingCardToPlayer(gamePlayer.Id, deck);
                IEnumerable<PlayerCard> playerCards = await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id);
                gamePlayer.RoundScore = _playerCardProvider.CardScoreCount(playerCards);

                await _gamePlayerRepository.Update(gamePlayer);
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private List<int> CreateDeck()
        {
            try
            {
                List<int> deck = new List<int>();
                deck = InitialDeck.Cards.ConvertAll(CardToIntConverter);
                deck = ShuffleDeck(deck);
                return deck;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private async Task<List<int>> ResumeDeck(IEnumerable<GamePlayer> gamePlayers)
        {
            try
            {
                List<int> deck = new List<int>();
                deck = InitialDeck.Cards.ConvertAll(CardToIntConverter);
                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    IEnumerable<PlayerCard> playerCards = await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id);
                    deck = RemoveCardsOnHands(playerCards, deck);
                }

                deck = ShuffleDeck(deck);
                return deck;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private int CardToIntConverter(Card card)
        {
            int id;
            id = card.Id;
            return id;
        }

        private List<int> ShuffleDeck(List<int> cards)
        {
            try
            {
                List<int> shuffledCards = cards;

                Random random = new Random();
                int card1;
                int card2;
                int glass;

                for (int i = 0; i < shuffledCards.Count; i++)
                {
                    card1 = random.Next(shuffledCards.Count);
                    card2 = random.Next(shuffledCards.Count);

                    glass = shuffledCards[card1];
                    shuffledCards[card1] = shuffledCards[card2];
                    shuffledCards[card2] = glass;
                }

                return shuffledCards;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }

        private List<int> RemoveCardsOnHands(IEnumerable<PlayerCard> playerCards, List<int> cards)
        {
            try
            {
                List<int> returnCards = cards;

                foreach (PlayerCard playerCard in playerCards)
                {
                    if (returnCards.Contains(playerCard.CardId))
                    {
                        returnCards.Remove(playerCard.CardId);
                    }
                }

                return returnCards;
            }
            catch (Exception ex)
            {
                string message = String.Format(ex.Source + "|" + ex.TargetSite + "|" + ex.StackTrace + "|" + ex.Message);
                _logger.Error(message);
                throw;
            }
        }
    }
}
