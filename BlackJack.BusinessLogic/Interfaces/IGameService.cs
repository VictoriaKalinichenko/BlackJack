using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<GamePlayerViewModel> GetGamePlayer(int gamePlayerId);

        Task<GameViewModel> GetGame(int gameId);

        Task<GamePlayerViewModel> GetDealerInFirstPhase(int gamePlayerId);

        Task<int> BetsCreation(int bet, int inGameId);

        Task<bool> RoundFirstPhase(int gameId);

        Task AddOneMoreCardToHuman(int gameId);

        Task<bool> CanHumanTakeOneMoreCard(int gameId);

        Task RoundSecondPhase(int gameId);

        Task<GameStartViewModel> GenerateGameStartViewModel(int gameId);

        Task<GameViewModel> GenerateFirstPhaseGameViewModel(int gameId);

        Task HumanBjAndDealerBjDangerContinueRound(int gameId);

        Task BetPayments(int gameId);
    }
}
