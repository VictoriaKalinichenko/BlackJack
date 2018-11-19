using BlackJack.ViewModels.Round;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services.Interfaces
{
    public interface IRoundService
    {
        Task<ResponseStartRoundView> Start(RequestStartRoundView view);

        Task<TakeCardRoundView> TakeCard(long gameId);

        Task<EndRoundView> End(long gameId);
    }
}
