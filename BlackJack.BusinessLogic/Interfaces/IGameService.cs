using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        int BetsCreation(BetInputViewModel betInputViewModel);

        GameViewModel RoundFirstPhase(int gameId);

        GameStartViewModel GenerateGameStartViewModel(int gameId);

        GameViewModel GenerateFirstPhaseGameViewModel(int gameId);
    }
}
