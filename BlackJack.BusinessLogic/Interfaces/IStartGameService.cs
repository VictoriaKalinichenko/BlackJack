using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartGameService
    {
        string ValidatePlayerName(string name);

        Task<int> CreateGame(int playerId, int amountOfBots);

        Task CreatePlayer(string name);

        Task<StartGameAuthorizePlayerView> AuthorizePlayer(string name);

        Task<int> ResumeGame(int playerId);

        Task<StartGameStartRoundView> GetStartGameStartRoundView(int gameId);
    }
}
