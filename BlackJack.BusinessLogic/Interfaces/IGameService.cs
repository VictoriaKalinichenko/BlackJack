using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<int> BetsCreation(BetInputViewModel betInputViewModel);

        Task<bool> RoundFirstPhase(int gameId);

        Task<GameStartViewModel> GenerateGameStartViewModel(int gameId);

        Task<GameViewModel> GenerateFirstPhaseGameViewModel(int gameId);
    }
}
