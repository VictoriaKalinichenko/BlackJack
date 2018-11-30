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

        public async Task<ResponseStartRoundView> Start(RequestStartRoundView requestView)
        {
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(requestView.GameId);
            Game game = await _gameRepository.Get(requestView.GameId);

            if (requestView.IsNewRound)
            {
                await RemoveCards(players, game.Id);
                await DistributeCards(players, CardValue.TwoCardsPerPlayer);
                CountCardScoreForPlayers(players);
                await _gamePlayerRepository.UpdateMany(players);
            }
            
            game.RoundResult = GameMessage.RoundInProcess;
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();

            if (human.CardScore >= CardValue.MaxCardScore)
            {
                GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
                await DistributeEndCardsForDealer(dealer);
                game.RoundResult = GetRoundResult(human, dealer);
                await _historyMessageManager.AddMessagesForRound(players, game.RoundResult, game.Id);
            }

            await _gameRepository.Update(game);

            ResponseStartRoundView responseView = CustomMapper.MapResponseStartRoundView(players, game.RoundResult);
            return responseView;
        }
        
        public async Task<TakeCardRoundView> TakeCard(long gameId)
        {
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            await DistributeCards(players, CardValue.OneCardPerPlayer, false);
            CountCardScoreForPlayers(players);
            await _gamePlayerRepository.UpdateMany(players);

            string roundResult = GameMessage.RoundInProcess;
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();

            if (human.CardScore >= CardValue.MaxCardScore)
            {
                GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
                await DistributeEndCardsForDealer(dealer);
                roundResult = GetRoundResult(human, dealer);
                Game game = CustomMapper.MapGame(gameId, roundResult);
                await _gameRepository.Update(game);

                await _historyMessageManager.AddMessagesForRound(players, roundResult, gameId);
            }

            TakeCardRoundView view = CustomMapper.MapTakeCardRoundView(players, roundResult);
            return view;
        }

        public async Task<EndRoundView> End(long gameId)
        {
            List<GamePlayer> players = await _gamePlayerRepository.GetAllByGameId(gameId);
            GamePlayer human = players.Where(m => m.Player.Type == PlayerType.Human).First();
            GamePlayer dealer = players.Where(m => m.Player.Type == PlayerType.Dealer).First();
            await DistributeEndCardsForDealer(dealer);

            string roundResult = GetRoundResult(human, dealer);
            Game game = CustomMapper.MapGame(gameId, roundResult);
            await _gameRepository.Update(game);

            await _historyMessageManager.AddMessagesForRound(players, roundResult, gameId);

            EndRoundView view = CustomMapper.MapEndRoundView(dealer, roundResult);
            return view;
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

        private async Task DistributeCards(List<GamePlayer> players, int cardAmountPerPlayer, bool doesDealerNeedCards = true)
        {
            var createdPlayerCards = new List<PlayerCard>();
            int cardAmount = players.Count() * cardAmountPerPlayer;
            List<Card> deck = await _cardRepository.GetSpecifiedAmount(cardAmount);

            foreach (GamePlayer player in players)
            {
                if (doesDealerNeedCards || player.Player.Type != PlayerType.Dealer)
                {
                    List<Card> cards = PopCardsFromDeck(deck, cardAmountPerPlayer);
                    List<PlayerCard> createdPlayerCardsForPlayer = CustomMapper.MapPlayerCards(player, cards);
                    player.PlayerCards.AddRange(createdPlayerCardsForPlayer);
                    createdPlayerCards.AddRange(createdPlayerCardsForPlayer);
                }
            }

            await _playerCardRepository.CreateMany(createdPlayerCards);
        }

        private async Task DistributeEndCardsForDealer(GamePlayer dealer)
        {
            var createdPlayerCards = new List<PlayerCard>();
            List<Card> deck = await _cardRepository.GetSpecifiedAmount(CardValue.AmountOfEndCardsForDealer);

            for (int iterator = deck.Count(); iterator > 0 && dealer.CardScore < CardValue.MaxDealerCardScore; iterator--)
            {
                Card card = PopCardsFromDeck(deck, CardValue.OneCard).First();
                PlayerCard createdPlayerCard = CustomMapper.MapPlayerCard(dealer, card);
                dealer.PlayerCards.Add(createdPlayerCard);
                createdPlayerCards.Add(createdPlayerCard);
                dealer.CardScore = CountCardScore(dealer.PlayerCards);
            }

            await _gamePlayerRepository.Update(dealer);
            await _playerCardRepository.CreateMany(createdPlayerCards);
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
            int cardScore = 0;

            int aceCount = 0;
            foreach (PlayerCard playerCard in playerCards)
            {
                if (playerCard.Card.Worth == CardValue.AceFullWorth)
                {
                    aceCount++;
                }
                
                if (playerCard.Card.Worth != CardValue.AceFullWorth)
                {
                    cardScore += playerCard.Card.Worth;
                }
            }
            
            for (int iterator = aceCount; iterator > 0; iterator--)
            {
                int aceWorth = CardValue.AceFullWorth;
                if (cardScore >= CardValue.MaxCardScore)
                {
                    aceWorth = CardValue.AceOnePointWorth;
                }
                cardScore += aceWorth;
            }

            return cardScore;
        }
    }
}