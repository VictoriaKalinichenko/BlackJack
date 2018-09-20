using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameLogicService
    {
        Task<string> ValidateBet(int bet, int gamePlayerId);

        Task<GameLogicRoundFirstPhaseResponseView> DoRoundFirstPhase(int bet, int gameId);

        Task<GameLogicRoundFirstPhaseResponseView> ResumeGameAfterRoundFirstPhase(int gameId);

        Task<GameLogicAddOneMoreCardToHumanView> AddOneMoreCardToHuman(int gameId);

        Task<GameLogicRoundSecondPhaseResponseView> DoRoundSecondPhase(int gameId, bool blackJackDangerContinueRound = false);

        Task<GameLogicRoundSecondPhaseResponseView> ResumeGameAfterRoundSecondPhase(int gameId);

        Task EndRound(int gameId);

        Task EndGame(GameLogicEndGameView endGameViewModel);
    }
}
