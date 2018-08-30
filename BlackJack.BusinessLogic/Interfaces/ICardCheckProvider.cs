namespace BlackJack.BusinessLogic.Interfaces
{
    public interface ICardCheckProvider
    {
        bool DealerBjDanger(int dealerFirstCard);

        float RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBjDanger);

        float RoundSecondPhaseResult(int bet, int score, int amountOfCards, int dealerScore);

        bool HumanPlayerHasEnoughCards(int score);

        bool BotHasEnoughCards(int score);
    }
}
