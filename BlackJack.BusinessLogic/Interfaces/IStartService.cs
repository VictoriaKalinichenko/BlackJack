using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels.Start;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartService
    {
        string ValidatePlayerName(string name);

        Task<long> CreateGame(long playerId, int amountOfBots);

        Task CreatePlayer(string name);

        Task<AuthorizePlayerViewModel> AuthorizePlayer(string name);

        Task<long> ResumeGame(long playerId);

        Task<BeginRoundViewModel> GetBeginRoundViewModel(long gameId);
    }
}
