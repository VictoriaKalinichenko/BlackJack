using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels.Game;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<string> ValidateBet(int bet, long gamePlayerId);

        Task<StartRoundResponseViewModel> StartRound(StartRoundRequestViewModel startRoundRequestViewModel);

        Task<StartRoundResponseViewModel> ResumeAfterStartRound(long gameId);

        Task<AddCardViewModel> AddCard(long gameId);

        Task<ContinueRoundResponseViewModel> ContinueRound(ContinueRoundRequestViewModel continueRoundRequestViewModel);

        Task<ContinueRoundResponseViewModel> ResumeAfterContinueRound(long gameId);

        Task EndRound(long gameId);

        Task EndGame(EndGameViewModel endGameViewModel);
    }
}
