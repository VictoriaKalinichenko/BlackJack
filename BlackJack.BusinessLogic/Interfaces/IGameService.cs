using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<int> BetsCreation(BetInputViewModel betInputViewModel);

        Task<bool> RoundFirstPhase(int gameId);

        Task AddOneMoreCardToHuman(int gameId);

        Task<bool> CanHumanTakeOneMoreCard(int gameId);

        Task RoundSecondPhase(int gameId);

        Task<GameStartViewModel> GenerateGameStartViewModel(int gameId);

        Task<GameViewModel> GenerateFirstPhaseGameViewModel(int gameId);

        Task HumanBjAndDealerBjDangerContinueRound(int humanGamePlayerId);

        Task BetPayments(int gameId);
    }
}
