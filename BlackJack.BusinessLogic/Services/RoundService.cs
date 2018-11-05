using AutoMapper;
using BlackJack.BusinessLogic.Constants;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Mappers;
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
            Game game = CustomMapper.GetGame(gameId, string.Empty);
            await _gameRepository.Update(game);

            await RemoveCards(players, gameId);
            await DistributeCards(players, CardValue.TwoCardsPerPlayer);
            CountCardScoreForPlayers(players);
            await _gamePlayerRepository.UpdateMany(players);
            await _historyMessageManager.AddMessagesForStartRound(players, gameId);

            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            bool canTakeCard = true;
            if (human.CardScore >= CardValue.MaxCardScore)
            {
                canTakeCard = false;
            }

            StartRoundView startRoundView = CustomMapper.GetStartRoundView(players, canTakeCard);
            return startRoundView;
        }
        
        public async Task<AddCardRoundView> AddCard(long gameId)
        {
            GamePlayer human = await _gamePlayerRepository.GetHumanByGameId(gameId);

            Card card = (await _cardRepository.GetSpecifiedAmount(CardValue.OneCardPerPlayer)).First();
            PlayerCard createdPlayerCard = CustomMapper.GetPlayerCard(human, card);
            human.PlayerCards.Add(createdPlayerCard);
            await _playerCardRepository.Create(createdPlayerCard);

            human.CardScore = CountCardScore(human.PlayerCards);
            await _gamePlayerRepository.Update(human);

            AddCardRoundView addCardRoundView = Mapper.Map<GamePlayer, AddCardRoundView>(human);
            addCardRoundView.CanTakeCard = true;
            if (human.CardScore >= CardValue.MaxCardScore)
            {
                addCardRoundView.CanTakeCard = false;
            }

            return addCardRoundView;
        }

        public async Task<ContinueRoundView> Continue(long gameId)
        {
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            List<PlayerCard> createdPlayerCards = await DistributeCards(players, CardValue.OneCardPerPlayer, false);
            CountCardScoreForPlayers(players);
            await _gamePlayerRepository.UpdateMany(players);

            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            string roundResult = GetRoundResult(human, dealer);

            Game game = CustomMapper.GetGame(gameId, roundResult);
            await _gameRepository.Update(game);

            await _historyMessageManager.AddMessagesForContinueRound(players, createdPlayerCards, roundResult, gameId);

            ContinueRoundView continueRoundView = CustomMapper.GetContinueRoundView(players, roundResult);
            return continueRoundView;
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
            
            RestoreRoundView restoreRoundView = CustomMapper.GetRestoreRoundView(players, canTakeCard);
            return restoreRoundView;
        }

        private async Task RemoveCards(List<GamePlayer> players, long gameId)
        {
            if (players.All(m => m.PlayerCards.Count() == 0))
            {
                return;
            }

            players.ForEach((player) =>
            {
                player.CardScore = 0;
                player.PlayerCards.Clear();
            });

            await _playerCardRepository.DeleteByGameId(gameId);
        }

        private async Task<List<PlayerCard>> DistributeCards(List<GamePlayer> players, int cardAmountPerPlayer, bool humanNeedsCards = true)
        {
            var createdPlayerCards = new List<PlayerCard>();
            int cardAmount = players.Count() * cardAmountPerPlayer;
            List<Card> deck = await _cardRepository.GetSpecifiedAmount(cardAmount);

            foreach (GamePlayer player in players)
            {
                if (humanNeedsCards || !(player.Player.Type == PlayerType.Human))
                {
                    List<Card> cards = PopCardsFromDeck(deck, cardAmountPerPlayer);
                    List<PlayerCard> createdPlayerCardsForPlayer = CustomMapper.GetPlayerCards(player, cards);
                    player.PlayerCards.AddRange(createdPlayerCardsForPlayer);
                    createdPlayerCards.AddRange(createdPlayerCardsForPlayer);
                }
            }

            await _playerCardRepository.CreateMany(createdPlayerCards);
            return createdPlayerCards;
        }

        private void CountCardScoreForPlayers(List<GamePlayer> players)
        {
            players.ForEach((player) =>
            {
                player.CardScore = CountCardScore(player.PlayerCards);
            });
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