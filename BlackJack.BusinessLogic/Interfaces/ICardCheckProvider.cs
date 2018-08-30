namespace BlackJack.BusinessLogic.Interfaces
{
    public interface ICardCheckProvider
    {
        bool DealerBjDanger(int dealerFirstCard);

        float RoundFirstPhaseResult(int score, int amountOfCards, bool dealerBjDanger);

        bool HumanPlayerHasEnoughCards(int score);

        bool BotHasEnoughCards(int score);
    }
}
