using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Providers.Interfaces;
using BlackJack.BLL.ViewModels;
using BlackJack.Entity.Enums;

namespace BlackJack.BLL.Providers
{
    public class CardCheckProvider : ICardCheckProvider
    {
        public bool FirstCardCheck(List<PlayerViewModel> players)
        {
            bool humanBjAndDealerBjDanger = false;

            PlayerViewModel dealer = players.Where(m => m.Player.IsDealer).First();
            bool dealerBjDanger = DealerBjDanger((int)dealer.Cards[0].CardName);

            foreach (PlayerViewModel player in players)
            {
                if (!player.Player.IsDealer)
                {
                    RoundResult roundResult = RoundFirstPhaseResult(player.GameScore.RoundScore, player.Cards.Count, dealerBjDanger);
                }
            }
            

            PlayerViewModel human = players.Where(m => m.Player.IsHuman).First();
            if (human.RoundResult == RoundResult.IsOneToOne)
            {
                human.RoundResult = RoundResult.Continue;
                humanBjAndDealerBjDanger = true;
            }

            return humanBjAndDealerBjDanger;
        }
        
        


        private RoundResult RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBjDanger)
        {
            RoundResult roundResult = new RoundResult();
            roundResult = RoundResult.Continue;

            if (!dealerBjDanger && PlayerBj(score, amountOfCards))
            {
                roundResult = RoundResult.IsBlackJack;
            }

            if (dealerBjDanger && PlayerBj(score, amountOfCards))
            {
                roundResult = RoundResult.IsOneToOne;
            }

            return roundResult;
        }
        

        private bool DealerBjDanger(int firstCardValue)
        {
            bool danger = false;

            if (firstCardValue >= Value.CardDealerBjDanger)
            {
                danger = true;
            }

            return danger;
        }

        private bool PlayerBj(int score, int amountOfCards)
        {
            bool result = false;

            if (score == Value.CardBjScore && amountOfCards == Value.CardBjAmount)
            {
                result = true;
            }

            return result;
        }

        private bool PlayerMoreThan21(int score)
        {
            bool result = false;

            if (score > Value.CardBjScore)
            {
                result = true;
            }

            return result;
        }

        private bool PlayerScoreEqualsDealerScore(int playerScore, int dealerScore)
        {
            bool result = false;

            if (playerScore == dealerScore && !PlayerMoreThan21(playerScore))
            {
                result = true;
            }

            return result;
        }

        private bool PlayerScoreBetterThanDealerScore(int playerScore, int dealerScore)
        {
            bool result = false;

            if (!PlayerMoreThan21(playerScore) && (playerScore > dealerScore || PlayerMoreThan21(dealerScore)))
            {
                result = true;
            }

            return result;
        }
    }
}
