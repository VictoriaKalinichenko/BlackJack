using System.Threading.Tasks;
using BlackJack.ViewModels.Round;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IRoundService
    {
        Task<ResponseStartRoundView> Start(RequestStartRoundView requestStartRoundView);

        Task<ResumeAfterStartRoundView> ResumeAfterStart(long gameId);

        Task<AddCardRoundView> AddCard(long gameId);

        Task<ResponseContinueRoundView> Continue(RequestContinueRoundView requestContinueRoundView);

        Task<ResumeAfterContinueRoundView> ResumeAfterContinue(long gameId);

        Task EndRound(long gameId);

        Task EndGame(EndGameRoundView endGameRoundView);
    }
}
