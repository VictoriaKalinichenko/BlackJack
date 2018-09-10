using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IPlayerLogicService
    {
        Task<GamePlayerViewModel> GetGamePlayer(int gamePlayerId);
        
        Task<GamePlayerViewModel> GetDealerInSecondPhase(int gamePlayerId);

        Task<GamePlayerViewModel> GetDealerInFirstPhase(int gamePlayerId);

        Task<int> BetsCreation(int bet, int inGameId);

        Task<string> BetValidation(int bet, int gamePlayerId);

        Task<string> HumanRoundResult(int gameId);

        Task OnRoundOver(int inGameId);

        Task<string> IsGameOver(int gameId);

        Task OnGameOver(int gameId, string gameResult);
    }
}
