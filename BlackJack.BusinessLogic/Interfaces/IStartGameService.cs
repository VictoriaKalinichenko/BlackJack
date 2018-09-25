using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels.StartGame;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartGameService
    {
        string ValidatePlayerName(string name);

        Task<long> CreateGame(long playerId, int amountOfBots);

        Task CreatePlayer(string name);

        Task<StartGameAuthorizePlayerView> AuthorizePlayer(string name);

        Task<long> ResumeGame(long playerId);

        Task<StartGameStartRoundView> GetStartGameStartRoundView(long gameId);
    }
}
