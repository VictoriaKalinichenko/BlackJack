using System.Threading.Tasks;
using BlackJack.ViewModels.Round;

namespace BlackJack.BusinessLogic.Services.Interfaces
{
    public interface IRoundService
    {
        Task<StartRoundView> Start(long gameId);

        Task<AddCardRoundView> TakeCard(long gameId);

        Task<EndRoundView> End(long gameId);

        Task<RestoreRoundView> Restore(long gameId);
    }
}
