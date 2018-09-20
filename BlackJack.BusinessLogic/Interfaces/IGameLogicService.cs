using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameLogicService
    {
        Task<string> ValidateBet(int bet, int gamePlayerId);

        Task<GameLogicDoRoundFirstPhaseResponseView> DoRoundFirstPhase(int bet, int gameId);

        Task<GameLogicResumeGameAfterRoundFirstPhaseView> ResumeGameAfterRoundFirstPhase(int gameId);

        Task<GameLogicAddOneMoreCardToHumanView> AddOneMoreCardToHuman(int gameId);

        Task<GameLogicDoRoundSecondPhaseResponseView> DoRoundSecondPhase(int gameId, bool blackJackDangerContinueRound = false);

        Task<GameLogicResumeGameAfterRoundSecondPhaseView> ResumeGameAfterRoundSecondPhase(int gameId);

        Task EndRound(int gameId);

        Task EndGame(GameLogicEndGameView endGameViewModel);
    }
}
