using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameLogicService
    {
        Task<string> ValidateBet(int bet, long gamePlayerId);

        Task<GameLogicRoundFirstPhaseResponseView> DoRoundFirstPhase(int bet, long gameId);

        Task<GameLogicRoundFirstPhaseResponseView> ResumeGameAfterRoundFirstPhase(long gameId);

        Task<GameLogicAddCardToHumanView> AddCardToHuman(long gameId);

        Task<GameLogicRoundSecondPhaseResponseView> DoRoundSecondPhase(long gameId, bool blackJackDangerContinueRound = false);

        Task<GameLogicRoundSecondPhaseResponseView> ResumeGameAfterRoundSecondPhase(long gameId);

        Task EndRound(long gameId);

        Task EndGame(GameLogicEndGameView endGameViewModel);
    }
}
