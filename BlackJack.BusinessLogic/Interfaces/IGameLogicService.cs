using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameLogicService
    {
        Task RoundFirstPhase(int gameId);

        Task<bool> IsHumanBjAndDealerBjDanger(int gameId);

        Task AddOneMoreCardToHuman(int gameId);

        Task<bool> CanHumanTakeOneMoreCard(int gameId);

        Task RoundSecondPhase(int gameId);

        Task HumanBjAndDealerBjDangerContinueRound(int gameId);
    }
}
