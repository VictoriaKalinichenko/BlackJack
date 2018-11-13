using BlackJack.ViewModels.Start;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services.Interfaces
{
    public interface IStartService
    {
        Task<SearchGameStartView> SearchGame(string name);

        Task<long> CreateGame(CreateGameStartView createGameStartView);
    }
}
