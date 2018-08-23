using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Providers;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;
using BlackJack.Entities.Models;
using BlackJack.ViewModels.ViewModels;


namespace BlackJack.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IPlayerCardRepository _playerCardRepository;

        private readonly IGamePlayerProvider _gamePlayerProvider;
        private readonly IPlayerCardProvider _playerCardProvider;
        private readonly ICardCheckProvider _cardCheckProvider;
        
        public GameService()
        {
            _playerRepository = new PlayerRepository();
            _gameRepository = new GameRepository();
            _gamePlayerRepository = new GamePlayerRepository();
            _playerCardRepository = new PlayerCardRepository();

            _gamePlayerProvider = new GamePlayerProvider();
            _playerCardProvider = new PlayerCardProvider();
            _cardCheckProvider = new CardCheckProvider();
        }
        
        public int BetsCreation(BetInputViewModel betInputViewModel)
        {
            int gameId;

            Game game = _gameRepository.Get(betInputViewModel.Game.Id);
            List<GamePlayer> gamePlayers = _gamePlayerRepository.GetByGameId(game.Id);
            gamePlayers = _gamePlayerProvider.BetsCreation(gamePlayers, betInputViewModel.HumanBet);

            game.Stage++;
            _gameRepository.Update(game);

            gameId = game.Id;

            return gameId;
        }

        public GameViewModel RoundFirstPhase(int gameId)
        {
            GameViewModel gameViewModel = new GameViewModel();

            Game game = _gameRepository.Get(gameId);
            List<GamePlayer> gamePlayers = _gamePlayerRepository.GetWithPlayersByGameId(gameId);
            List<int> deck = CreateDeck();
            FirstCardsDistribution(gamePlayers, deck);
            FirstCardCheck(gamePlayers);

            game.Stage++;
            _gameRepository.Update(game);

            gameViewModel = GenerateFirstPhaseGameViewModel(game.Id);

            return gameViewModel;
        }

        public GameStartViewModel GenerateGameStartViewModel(int gameId)
        {
            GameStartViewModel gameStartViewModel = new GameStartViewModel();
            gameStartViewModel.Bots = new List<GamePlayerStartViewModel>();
            
            gameStartViewModel.Id = gameId;

            List<GamePlayer> gamePlayers = _gamePlayerRepository.GetByGameId(gameId);

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                PlayerViewModel playerViewModel = new PlayerViewModel();
                Player player = _gamePlayerRepository.GetPlayerByGamePlayerId(gamePlayer.Id);

                playerViewModel.Id = player.Id;
                playerViewModel.Name = player.Name;

                if (player.IsDealer)
                {
                    playerViewModel.PlayerType = PlayerType._dealer;
                    gameStartViewModel.Dealer = new GamePlayerStartViewModel();
                    gameStartViewModel.Dealer.Player = playerViewModel;
                    gameStartViewModel.Dealer.Score = gamePlayer.Score;
                }

                if (player.IsHuman)
                {
                    playerViewModel.PlayerType = PlayerType._human;
                    gameStartViewModel.Human = new GamePlayerStartViewModel();
                    gameStartViewModel.Human.Player = playerViewModel;
                    gameStartViewModel.Human.Score = gamePlayer.Score;
                }

                if (!player.IsHuman && !player.IsDealer)
                {
                    playerViewModel.PlayerType = PlayerType._bot;
                    gameStartViewModel.Bots.Add(new GamePlayerStartViewModel() { Player = playerViewModel, Score = gamePlayer.Score });
                }
            }

            return gameStartViewModel;
        }

        public GameViewModel GenerateFirstPhaseGameViewModel(int gameId)
        {
            GameViewModel gameViewModel = new GameViewModel();
            
            gameViewModel.Id = gameId;
            List<GamePlayer> gamePlayers = _gamePlayerRepository.GetByGameId(gameId);

            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                GamePlayerViewModel gamePlayerViewModel = new GamePlayerViewModel { Score = gamePlayer.Score };
                Player player = _gamePlayerRepository.GetPlayerByGamePlayerId(gamePlayer.Id);
                PlayerViewModel playerViewModel = new PlayerViewModel() { Id = player.Id, Name = player.Name };
                List<PlayerCard> playerCards = _playerCardRepository.GetByGamePlayerId(gamePlayer.Id);

                if (player.IsDealer)
                {
                    playerViewModel.PlayerType = PlayerType._dealer;
                    gamePlayerViewModel.Player = playerViewModel;
                    gamePlayerViewModel.Cards.Add(_playerCardProvider.PlayerCardToCardString(playerCards[0]));

                    gameViewModel.Dealer = gamePlayerViewModel;
                }

                if (!player.IsDealer)
                {
                    gamePlayerViewModel.CardScore = gamePlayer.RoundScore;
                    gamePlayerViewModel.Bet = gamePlayer.Bet;
                    gamePlayerViewModel.Cards = _playerCardProvider.GetCardsStringList(playerCards);
                }

                if (player.IsHuman)
                {
                    playerViewModel.PlayerType = PlayerType._human;
                    gamePlayerViewModel.Player = playerViewModel;

                    gameViewModel.Human = gamePlayerViewModel;
                }

                if (!player.IsHuman && !player.IsDealer)
                {
                    playerViewModel.PlayerType = PlayerType._bot;
                    gamePlayerViewModel.Player = playerViewModel;

                    gameViewModel.Bots.Add(gamePlayerViewModel);
                }
            }

            return gameViewModel;
        }
        
        private bool FirstCardCheck(List<GamePlayer> players)
        {
            bool humanBjAndDealerBjDanger = false;

            GamePlayer dealer = players.Where(m => m.Player.IsDealer).First();
            Card dealerFirstCard = InitialDeck._cards.Where(m => m.Id == dealer.PlayerCards[0].CardId).First();

            foreach (GamePlayer player in players)
            {
                if (!player.Player.IsDealer)
                {
                    player.BetPayCoefficient = _cardCheckProvider.RoundFirstPhaseResult(player.RoundScore, player.PlayerCards.Count, (int)dealerFirstCard.CardName);
                    _gamePlayerRepository.Update(player);
                }
            }

            GamePlayer human = players.Where(m => m.Player.IsHuman).First();
            if (human.BetPayCoefficient == BetValue._betWinCoefficient)
            {
                humanBjAndDealerBjDanger = true;
            }

            return humanBjAndDealerBjDanger;
        }

        private void FirstCardsDistribution(List<GamePlayer> players, List<int> deck)
        {
            foreach (GamePlayer gamePlayer in players)
            {
                AddingCardToPlayer(gamePlayer, deck);
                AddingCardToPlayer(gamePlayer, deck);
            }
        }
                
        private void AddingCardToPlayer(GamePlayer gamePlayer, List<int> deck)
        {
            _playerCardProvider.AddingCardToPlayer(gamePlayer.Id, deck);
            List<PlayerCard> playerCards = _playerCardRepository.GetByGamePlayerId(gamePlayer.Id);
            gamePlayer.RoundScore = _playerCardProvider.CardScoreCount(playerCards);
            
            _gamePlayerRepository.Update(gamePlayer);
        }
        
        private List<int> CreateDeck()
        {
            List<int> deck;
            deck = InitialDeck._cards.ConvertAll(CardToIntConverter);
            deck = ShuffleDeck(deck);

            return deck;
        }

        private List<int> ResumeDeck(List<GamePlayer> gamePlayers)
        {
            List<int> deck;

            deck = InitialDeck._cards.ConvertAll(CardToIntConverter);
            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                List<PlayerCard> playerCards = _playerCardRepository.GetByGamePlayerId(gamePlayer.Id);
                deck = RemoveCardsOnHands(playerCards, deck);
            }

            deck = ShuffleDeck(deck);

            return deck;
        }

        private int CardToIntConverter(Card card)
        {
            int id;
            id = card.Id;
            return id;
        }

        private List<int> ShuffleDeck(List<int> cards)
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

        private List<int> RemoveCardsOnHands(List<PlayerCard> playerCards, List<int> cards)
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
    }
}
