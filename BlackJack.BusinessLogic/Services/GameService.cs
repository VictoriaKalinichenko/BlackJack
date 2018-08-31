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
        private readonly ILogRepository _logRepository;

        private readonly IGamePlayerProvider _gamePlayerProvider;
        private readonly IPlayerCardProvider _playerCardProvider;
        private readonly ICardCheckProvider _cardCheckProvider;
        

        public GameService(IPlayerRepository playerRepository, IGameRepository gameRepository, IGamePlayerRepository gamePlayerRepository, IPlayerCardRepository playerCardRepository, 
            IGamePlayerProvider gamePlayerProvider, IPlayerCardProvider playerCardProvider, ICardCheckProvider cardCheckProvider, ILogRepository logRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;
            _logRepository = logRepository;

            _gamePlayerProvider = gamePlayerProvider;
            _playerCardProvider = playerCardProvider;
            _cardCheckProvider = cardCheckProvider;
        }
        
        public async Task<GamePlayerViewModel> GetGamePlayer(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayerViewModel = new GamePlayerViewModel();
                GamePlayer gamePlayer = await _gamePlayerRepository.Get(gamePlayerId);
                gamePlayerViewModel.Id = gamePlayer.Id;
                gamePlayerViewModel.Bet = gamePlayer.Bet;
                gamePlayerViewModel.Score = gamePlayer.Score;
                gamePlayerViewModel.CardScore = gamePlayer.RoundScore;
                List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
                gamePlayerViewModel.Cards = _playerCardProvider.GetCardsStringList(playerCards);
                return gamePlayerViewModel;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GamePlayerViewModel> GetDealerInFirstPhase(int gamePlayerId)
        {
            try
            {
                GamePlayerViewModel gamePlayerViewModel = new GamePlayerViewModel();
                GamePlayer gamePlayer = await _gamePlayerRepository.Get(gamePlayerId);
                gamePlayerViewModel.Id = gamePlayer.Id;
                gamePlayerViewModel.Score = gamePlayer.Score;
                List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
                gamePlayerViewModel.Cards = new List<string>();
                gamePlayerViewModel.Cards.Add(InitialDeck.Cards.Where(m => m.Id == playerCards[0].CardId).First().ToString());
                return gamePlayerViewModel;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GameViewModel> GetGame(int gameId)
        {
            try
            {
                GameViewModel game = new GameViewModel();
                game.Bots = new List<PlayerViewModel>();
                game.Id = gameId;

                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    PlayerViewModel playerViewModel = new PlayerViewModel();

                    playerViewModel.Id = gamePlayer.Id;
                    playerViewModel.Name = gamePlayer.Player.Name;
                    playerViewModel.Score = gamePlayer.Score;

                    if (gamePlayer.Player.IsDealer)
                    {
                        game.Dealer = playerViewModel;
                    }

                    if (gamePlayer.Player.IsHuman)
                    {
                        game.Human = playerViewModel;
                    }

                    if (!gamePlayer.Player.IsHuman && !gamePlayer.Player.IsDealer)
                    {
                        game.Bots.Add(playerViewModel);
                    }
                }

                return game;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }



        public async Task<int> BetsCreation(int bet, int inGameId)
        {
            try
            {
                int outGameId = 0;

                Game game = await _gameRepository.Get(inGameId);
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(game.Id);
                await _gamePlayerProvider.BetsCreation(gamePlayers, bet);

                outGameId = game.Id;
                game.Stage = Stage.BetsCreation;
                await _gameRepository.Update(game);
                await _logRepository.CreateLogGameStageIsChanged(game.Id, game.Stage);

                return outGameId;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<bool> RoundFirstPhase(int gameId)
        {
            try
            {
                await _logRepository.CreateLogRoundIsStarted(gameId);

                bool humanBjAndDealerBjDanger = false;

                Game game = await _gameRepository.Get(gameId);
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                List<int> deck = CreateDeck();

                await FirstCardsDistribution(gamePlayers, deck);

                humanBjAndDealerBjDanger = await FirstCardCheck(gamePlayers);

                game.Stage = Stage.FirstCardsDistribution;
                await _gameRepository.Update(game);
                await _logRepository.CreateLogGameStageIsChanged(game.Id, game.Stage);

                return humanBjAndDealerBjDanger;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<bool> CanHumanTakeOneMoreCard(int gameId)
        {
            try
            {
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                GamePlayer human = gamePlayers.Where(g => g.Player.IsHuman).FirstOrDefault();

                bool canHumanTakeOneMoreCard = false;
                canHumanTakeOneMoreCard = !_cardCheckProvider.HumanPlayerHasEnoughCards(human.RoundScore);                
                return canHumanTakeOneMoreCard;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task RoundSecondPhase(int gameId)
        {
            try
            {
                Game game = await _gameRepository.Get(gameId);
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                List<int> deck = CreateDeck();

                await SecondCardsDistribution(gamePlayers, deck);

                await SecondCardCheck(gamePlayers);

                game.Stage = Stage.SecondCardsDistribution;
                await _gameRepository.Update(game);
                await _logRepository.CreateLogGameStageIsChanged(game.Id, game.Stage);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task<GameViewModel> GenerateFirstPhaseGameViewModel(int gameId)
        {
            try
            {
                GameViewModel gameViewModel = new GameViewModel();
                //gameViewModel.Bots = new List<GamePlayerViewModel>();
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

                        //gameViewModel.Dealer = gamePlayerViewModel;
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

                        //gameViewModel.Human = gamePlayerViewModel;
                    }

                    if (!gamePlayer.Player.IsHuman && !gamePlayer.Player.IsDealer)
                    {
                        playerViewModel.PlayerType = PlayerType.Bot;
                        gamePlayerViewModel.Player = playerViewModel;

                        //gameViewModel.Bots.Add(gamePlayerViewModel);
                    }
                }

                return gameViewModel;
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task HumanBjAndDealerBjDangerContinueRound(int gameId)
        {
            try
            {
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                GamePlayer human = gamePlayers.Where(m => m.Player.IsHuman).FirstOrDefault();
                human.BetPayCoefficient = BetValue.BetDefaultCoefficient;
                await _gamePlayerRepository.Update(human);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task BetPayments(int gameId)
        {
            try
            {
                IEnumerable<GamePlayer> gamePlayers = await _gamePlayerRepository.GetByGameId(gameId);
                await _gamePlayerProvider.RoundBetPayments(gamePlayers);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
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
                bool dealerBjDanger = _cardCheckProvider.DealerBjDanger((int)dealerFirstCard.CardName);

                if(dealerBjDanger)
                {
                    await _logRepository.CreateLogDealerBjDanger(gamePlayers.First().GameId, dealer.Player, dealerFirstCard.Id, (int)dealerFirstCard.CardName, dealerFirstCard.ToString());
                }

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    if (!gamePlayer.Player.IsDealer)
                    {
                        List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
                        gamePlayer.BetPayCoefficient = _cardCheckProvider.RoundFirstPhaseResult(gamePlayer.RoundScore, playerCards.Count, dealerBjDanger);
                        await _gamePlayerRepository.Update(gamePlayer);

                        if (gamePlayer.BetPayCoefficient == BetValue.BetBjCoefficient)
                        {
                            await _logRepository.CreateLogPlayerBj(gamePlayer.Id, gamePlayer.Player, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient);
                        }

                        if (gamePlayer.BetPayCoefficient == BetValue.BetWinCoefficient)
                        {
                            await _logRepository.CreateLogPlayerBjAndDealerBjDanger(gamePlayer.Id, gamePlayer.Player, gamePlayer.RoundScore, gamePlayer.BetPayCoefficient);
                        }
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private async Task SecondCardsDistribution(IEnumerable<GamePlayer> players, List<int> deck)
        {
            try
            {
                foreach (GamePlayer gamePlayer in players)
                {
                    await SecondCardAddingToBot(gamePlayer, deck);
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private async Task SecondCardCheck(IEnumerable<GamePlayer> gamePlayers)
        {
            try
            {
                GamePlayer dealer = gamePlayers.Where(m => m.Player.IsDealer).FirstOrDefault();

                foreach (GamePlayer gamePlayer in gamePlayers)
                {
                    if (!gamePlayer.Player.IsDealer)
                    {
                        List<PlayerCard> playerCards = (await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id)).ToList();
                        gamePlayer.BetPayCoefficient = _cardCheckProvider.RoundSecondPhaseResult(gamePlayer.Bet, gamePlayer.RoundScore, playerCards.Count, dealer.RoundScore);
                        await _gamePlayerRepository.Update(gamePlayer);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private async Task AddingCardToPlayer(GamePlayer gamePlayer, List<int> deck)
        {
            try
            {
                int cardId = await _playerCardProvider.AddingCardToPlayer(gamePlayer.Id, deck);
                IEnumerable<PlayerCard> playerCards = await _playerCardRepository.GetByGamePlayerId(gamePlayer.Id);
                gamePlayer.RoundScore = _playerCardProvider.CardScoreCount(playerCards);

                await _gamePlayerRepository.Update(gamePlayer);
                await _logRepository.CreateLogCardIsAdded(gamePlayer.GameId, gamePlayer.Player, gamePlayer.RoundScore, cardId, (int)InitialDeck.Cards[cardId].CardName, InitialDeck.Cards[cardId].ToString());
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private async Task SecondCardAddingToBot(GamePlayer gamePlayer, List<int> deck)
        {
            try
            {
                for (; !_cardCheckProvider.BotHasEnoughCards(gamePlayer.RoundScore);)
                {
                    await AddingCardToPlayer(gamePlayer, deck);
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
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
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
    }
}
