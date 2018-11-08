using System.Threading.Tasks;
using BlackJack.ViewModels.Round;

namespace BlackJack.BusinessLogic.Services.Interfaces
{
    public interface IRoundService
    {
        Task<StartRoundView> Start(long gameId);

        Task<TakeCardRoundView> TakeCard(long gameId);

        Task<string> End(long gameId);

        Task<RestoreRoundView> Restore(long gameId);
    }
}
