using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IPlayerLogicService
    {
        Task<GetPlayerViewModel> GetGamePlayer(int gamePlayerId);
        
        Task<GetPlayerViewModel> GetDealerInSecondPhase(int gamePlayerId);

        Task<GetPlayerViewModel> GetDealerInFirstPhase(int gamePlayerId);

        Task<int> BetsCreation(int bet, int inGameId);

        Task<string> BetValidation(int bet, int gamePlayerId);

        Task<string> HumanRoundResult(int gameId);

        Task OnRoundOver(int inGameId);

        Task<string> IsGameOver(int gameId);

        Task OnGameOver(int gameId, string gameResult);
    }
}
