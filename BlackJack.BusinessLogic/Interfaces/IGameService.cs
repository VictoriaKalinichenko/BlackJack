using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels.Game;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<string> ValidateBet(int bet, long gamePlayerId);

        Task<DoRoundFirstPhaseResponseViewModel> DoRoundFirstPhase(int bet, long gameId);

        Task<DoRoundFirstPhaseResponseViewModel> ResumeAfterRoundFirstPhase(long gameId);

        Task<AddCardViewModel> AddCard(long gameId);

        Task<DoRoundSecondPhaseResponseViewModel> DoRoundSecondPhase(long gameId, bool blackJackDangerContinueRound = false);

        Task<DoRoundSecondPhaseResponseViewModel> ResumeAfterRoundSecondPhase(long gameId);

        Task EndRound(long gameId);

        Task EndGame(EndGameViewModel endGameViewModel);
    }
}
