using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.ViewModels.Enums;

namespace BlackJack.BusinessLogic.Providers
{
    public class PlayerCardProvider : IPlayerCardProvider
    {
        private readonly IPlayerCardRepository _playerCardRepository;


        public PlayerCardProvider(IPlayerCardRepository playerCardRepository)
        {
            _playerCardRepository = playerCardRepository;
        }
        
        public string ConvertCardToString(Card card)
        {
            string result = string.Empty;
            result = $"{((CardName)card.CardName).ToString()} {((CardType)card.CardType).ToString()}";
            return result;
        }

        public List<string> GetCardsStringList(IEnumerable<PlayerCard> playerCards)
        {
            var cardsStringList = playerCards.ToList().ConvertAll(PlayerCardToCardString);
            return cardsStringList;
        }

        public async Task<Card> AddingCardToPlayer(int gamePlayerId, List<Card> deck)
        {
            Card card = deck.First();
            deck.Remove(card);

            PlayerCard playerCard = new PlayerCard()
            {
                CardId = card.Id,
                GamePlayerId = gamePlayerId
            };
            await _playerCardRepository.Create(playerCard);
            return card;
        }

        public int CardScoreCount(IEnumerable<PlayerCard> playerCards)
        {
            int roundScore = 0;

            foreach (PlayerCard playerCard in playerCards)
            {
                int cardScore = playerCard.Card.CardName;
                if (cardScore > (int)CardName.Ace)
                {
                    cardScore = (int)CardName.Ten;
                }

                roundScore += cardScore;
            }

            int aceCount = playerCards
                .Where(m => m.Card.CardName == (int)CardName.Ace)
                .Count();

            for (; aceCount > 0 && roundScore > CardValueHelper.CardBjScore;)
            {
                aceCount--;
                roundScore -= (int)CardName.Ten;
            }

            return roundScore;
        }
        
        private string PlayerCardToCardString(PlayerCard playerCard)
        {
            string result = ConvertCardToString(playerCard.Card);
            return result;
        }
    }
}
