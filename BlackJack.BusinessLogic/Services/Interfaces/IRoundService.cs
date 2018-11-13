using BlackJack.ViewModels.Round;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services.Interfaces
{
    public interface IRoundService
    {
        Task<StartRoundView> Start(long gameId);

        Task<TakeCardRoundView> TakeCard(long gameId);

        Task<EndRoundView> End(long gameId);

        Task<RestoreRoundView> Restore(long gameId);
    }
}
