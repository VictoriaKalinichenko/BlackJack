using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels.Start;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartService
    {
        Task<long> CreateGame(long playerId, int amountOfBots);

        Task CreatePlayer(string name);

        Task<StartAuthorizePlayerViewModel> AuthorizePlayer(string name);

        Task<long> ResumeGame(long playerId);

        Task<StartInitRoundViewModel> InitRound(long gameId);
    }
}
