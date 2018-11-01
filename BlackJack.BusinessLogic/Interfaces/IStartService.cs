using System.Threading.Tasks;
using BlackJack.ViewModels.Start;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartService
    {
        Task<long> CreateGame(long playerId, int amountOfBots);

        Task CreatePlayer(string name);

        Task<AuthorizePlayerStartView> AuthorizePlayer(string name);

        Task<long> ResumeGame(long playerId);

        Task<InitializeStartView> InitializeRound(long gameId);
    }
}
