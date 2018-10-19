using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels.Game;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<string> ValidateBet(int bet, long gamePlayerId);

        Task<StartRoundResponseViewModel> StartRound(StartRoundRequestViewModel startRoundRequestViewModel);

        Task<StartRoundResponseViewModel> ResumeAfterStartRound(long gameId);

        Task<AddCardGameView> AddCard(long gameId);

        Task<ContinueRoundResponseViewModel> ContinueRound(RequestContinueRoundGameView continueRoundRequestViewModel);

        Task<ContinueRoundResponseViewModel> ResumeAfterContinueRound(long gameId);

        Task EndRound(long gameId);

        Task EndGame(EndGameView endGameViewModel);
    }
}
