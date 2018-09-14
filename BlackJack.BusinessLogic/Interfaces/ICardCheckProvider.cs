namespace BlackJack.BusinessLogic.Interfaces
{
    public interface ICardCheckProvider
    {
        bool DealerBlackJackDanger(int dealerFirstCard);

        float RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBlackJackDanger);

        float RoundSecondPhaseResult(int score, int amountOfCards, int dealerScore, int dealerAmountOfCards, float betPayCoefficient);

        bool HumanPlayerHasEnoughCards(int score);

        bool BotHasEnoughCards(int score);

        string HumanRoundResult(float betPayCoefficient);
    }
}
