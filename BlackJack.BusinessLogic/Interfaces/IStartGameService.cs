using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartGameService
    {
        string PlayerNameValidation(string name);

        Task<int> CreateGame(int playerId, int amountOfBots);

        Task<string> PlayerCreation(string name);

        Task<AuthorizedPlayerViewModel> PlayerAuthorization(string name);

        Task<int> ResumeGame(int playerId);

        Task<RoundViewModel> GetGame(int gameId);
    }
}
