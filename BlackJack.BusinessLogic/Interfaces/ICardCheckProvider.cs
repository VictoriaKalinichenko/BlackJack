namespace BlackJack.BusinessLogic.Interfaces
{
    public interface ICardCheckProvider
    {
        float RoundFirstPhaseResult(int score, int amountOfCards, int dealerFirstCard);

        bool HumanPlayerHasEnoughCards(int score);

        bool BotHasEnoughCards(int score);
    }
}
