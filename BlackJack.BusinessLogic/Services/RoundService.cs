using AutoMapper;
using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Managers.Interfaces;
using BlackJack.BusinessLogic.Mappers;
using BlackJack.BusinessLogic.Services.Interfaces;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using BlackJack.ViewModels.Round;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class RoundService : IRoundService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGamePlayerRepository _gamePlayerRepository;
        private readonly IPlayerCardRepository _playerCardRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IHistoryMessageManager _historyMessageManager;

        public RoundService(IPlayerRepository playerRepository, IGameRepository gameRepository, 
            IGamePlayerRepository gamePlayerRepository, IPlayerCardRepository playerCardRepository, 
            ICardRepository cardRepository, IHistoryMessageManager historyMessageManager)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _gamePlayerRepository = gamePlayerRepository;
            _playerCardRepository = playerCardRepository;
            _cardRepository = cardRepository;
            _historyMessageManager = historyMessageManager;
        }

        public async Task<StartRoundView> Start(long gameId)
        {  
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            Game game = CustomMapper.MapGame(gameId, string.Empty);
            await _gameRepository.Update(game);

            players = await RemoveCards(players, gameId);
            players = await DistributeCards(players, CardValue.TwoCardsPerPlayer);
            players = CountCardScoreForPlayers(players);
            await _gamePlayerRepository.UpdateMany(players);

            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            bool canTakeCard = true;
            if (human.CardScore >= CardValue.MaxCardScore)
            {
                canTakeCard = false;
            }

            StartRoundView view = CustomMapper.MapStartRoundView(players, canTakeCard);
            return view;
        }
        
        public async Task<AddCardRoundView> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetHumanByGameId(gameId);

            Card card = (await _cardRepository.GetSpecifiedAmount(CardValue.OneCardPerPlayer)).First();
            PlayerCard createdPlayerCard = CustomMapper.MapPlayerCard(human, card);
            human.PlayerCards.Add(createdPlayerCard);
            await _playerCardRepository.Create(createdPlayerCard);

            human.CardScore = CountCardScore(human.PlayerCards);
            await _gamePlayerRepository.Update(human);

            AddCardRoundView view = Mapper.Map<GamePlayer, AddCardRoundView>(human);
            view.CanTakeCard = true;
            if (human.CardScore >= CardValue.MaxCardScore)
            {
                view.CanTakeCard = false;
            }

            return view;
        }

        public async Task<ContinueRoundView> Continue(long gameId)
        {
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            players = await DistributeCards(players, CardValue.OneCardPerPlayer, false);
            players = CountCardScoreForPlayers(players);
            await _gamePlayerRepository.UpdateMany(players);

            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            string roundResult = GetRoundResult(human, dealer);

            Game game = CustomMapper.MapGame(gameId, roundResult);
            await _gameRepository.Update(game);
            await _historyMessageManager.AddMessagesForRound(players, roundResult, gameId);

            ContinueRoundView view = CustomMapper.MapContinueRoundView(players, roundResult);
            return view;
        }

        public async Task<RestoreRoundView> Restore(long gameId)
        {
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            bool canTakeCard = true;
            if (human.CardScore >= CardValue.MaxCardScore)
            {
                canTakeCard = false;
            }
            
            RestoreRoundView view = CustomMapper.MapRestoreRoundView(players, canTakeCard);
            return view;
        }

        private async Task<List<GamePlayer>> RemoveCards(List<GamePlayer> players, long gameId)
        {
            List<GamePlayer> returnedGamePlayers = players;
            if (returnedGamePlayers.All(m => m.PlayerCards.Count() == 0))
            {
                return returnedGamePlayers;
            }

            returnedGamePlayers.ForEach((player) =>
            {
                player.CardScore = 0;
                player.PlayerCards.Clear();
            });

            await _playerCardRepository.DeleteByGameId(gameId);
            return returnedGamePlayers;
        }

        private async Task<List<GamePlayer>> DistributeCards (List<GamePlayer> players, int cardAmountPerPlayer, bool humanNeedsCards = true)
        {
            List<GamePlayer> returnedGamePlayers = players;
            var createdPlayerCards = new List<PlayerCard>();
            int cardAmount = returnedGamePlayers.Count() * cardAmountPerPlayer;
            List<Card> deck = await _cardRepository.GetSpecifiedAmount(cardAmount);

            foreach (GamePlayer player in returnedGamePlayers)
            {
                if (humanNeedsCards || !(player.Player.Type == PlayerType.Human))
                {
                    List<Card> cards = PopCardsFromDeck(deck, cardAmountPerPlayer);
                    List<PlayerCard> createdPlayerCardsForPlayer = CustomMapper.MapPlayerCards(player, cards);
                    player.PlayerCards.AddRange(createdPlayerCardsForPlayer);
                    createdPlayerCards.AddRange(createdPlayerCardsForPlayer);
                }
            }

            await _playerCardRepository.CreateMany(createdPlayerCards);
            return returnedGamePlayers;
        }

        private List<GamePlayer> CountCardScoreForPlayers(List<GamePlayer> players)
        {
            List<GamePlayer> returnedGamePlayers = players;
            returnedGamePlayers.ForEach((player) =>
            {
                player.CardScore = CountCardScore(player.PlayerCards);
            });
            return returnedGamePlayers;
        }
        
        private string GetRoundResult(GamePlayer human, GamePlayer dealer)
        {
            string roundResult = GameMessage.Lose;
            
            if (human.CardScore <= CardValue.MaxCardScore && 
               (human.CardScore > dealer.CardScore || dealer.CardScore > CardValue.MaxCardScore))
            {
                roundResult = GameMessage.Win;
            }

            if (human.CardScore == dealer.CardScore && human.CardScore <= CardValue.MaxCardScore)
            {
                roundResult = GameMessage.Equal;
            }

            return roundResult;
        }

        private List<Card> PopCardsFromDeck(List<Card> deck, int cardAmount)
        {
            List<Card> cards = deck.GetRange(CardValue.FirstItemIndex, cardAmount);
            deck.RemoveRange(CardValue.FirstItemIndex, cardAmount);
            return cards;
        }

        private int CountCardScore(List<PlayerCard> playerCards)
        {
            int roundScore = 0;

            int aceCount = 0;
            foreach (PlayerCard playerCard in playerCards)
            {
                if (playerCard.Card.Worth == CardValue.AceFullWorth)
                {
                    aceCount++;
                }
                
                if (playerCard.Card.Worth != CardValue.AceFullWorth)
                {
                    roundScore += playerCard.Card.Worth;
                }
            }
            
            for (int iterator = aceCount; iterator > 0; iterator--)
            {
                int aceWorth = CardValue.AceFullWorth;
                if (roundScore >= CardValue.MaxCardScore)
                {
                    aceWorth = CardValue.AceOnePointWorth;
                }
                roundScore += aceWorth;
            }

            return roundScore;
        }
    }
}