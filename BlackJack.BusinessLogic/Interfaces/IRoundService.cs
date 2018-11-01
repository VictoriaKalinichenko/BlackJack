using System.Threading.Tasks;
using BlackJack.ViewModels.Round;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IRoundService
    {
        Task<StartRoundView> Start(long gameId);

        Task<AddCardRoundView> AddCard(long gameId);

        Task<ContinueRoundView> Continue(long gameId);

        Task<RestoreRoundView> Restore(long gameId);
    }
}
