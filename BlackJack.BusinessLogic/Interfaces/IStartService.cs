using System.Threading.Tasks;
using BlackJack.ViewModels.Start;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartService
    {
        Task<IndexStartView> SearchGameForPlayer(string name);

        Task<long> CreateGame(CreateGameStartView createGameStartView);

        Task<InitializeStartView> InitializeRound(long gameId);
    }
}
