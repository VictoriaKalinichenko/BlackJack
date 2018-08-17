using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Services;
using BlackJack.BLL.Services.Interfaces;
using BlackJack.BLL.ViewModels;
using BlackJack.BLL.Providers.Interfaces;
using BlackJack.Entity.Enums;

namespace BlackJack.BLL.Providers
{
    public class CardDistributionProvider : ICardDistributionProvider
    {
        private IGameService GameService;

        public CardDistributionProvider()
        {
            GameService = new GameService();
        }



        public List<Card> CreateDeck()
        {
            List<Card> cards;

            cards = InitialDeck.Cards;

            return cards;
        }

        

        public void FirstCardsDistribution(List<PlayerViewModel> players, List<Card> deck)
        {
            ShuffleDeck(deck);

            foreach (PlayerViewModel player in players)
            {
                AddingCardToPlayer(player, deck);
                AddingCardToPlayer(player, deck);
            }
        }

        public bool OneMoreCardToHuman(PlayerViewModel player, List<Card> deck = null, int takeCardKey = 0)
        {
            bool canTakeOneMoreCard = true;

            if (takeCardKey == 1)
            {
                AddingCardToPlayer(player, deck);
            }

            if (player.GameScore.RoundScore >= Value.CardBjScore)
            {
                canTakeOneMoreCard = false;
            }

            return canTakeOneMoreCard;
        }



        private void AddingCardToPlayer(PlayerViewModel player, List<Card> deck)
        {
            Card card;
            card = deck.First();
            deck.Remove(card);

            player.Cards.Add(card);
            CardScoreCount(player);

            List<int> cardIds = player.Cards.ConvertAll(CardToIntConverter);
            GameService.UpdatePlayerCards(player.GameScore, cardIds);
        }

        private void CardScoreCount(PlayerViewModel player)
        {
            int count = player.Cards.Sum(m => (int)m.CardName);
            int AceCount = player.Cards
                .Where(m => m.CardName == CardName.Ace)
                .Count();

            for (; AceCount > 0 && count > 21;)
            {
                AceCount--;
                count -= (int)CardName.Ten;
            }

            player.GameScore.RoundScore = count;
        }

        private int CardToIntConverter(Card card)
        {
            int id;

            id = card.Id;

            return id;
        }

        
        private List<Card> ShuffleDeck(List<Card> cards)
        {
            List<Card> shuffledCards = cards;

            Random random = new Random();
            int card1;
            int card2;
            Card glass;

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
    }
}
