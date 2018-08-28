using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartGameService
    {
        Task<string> PlayerNameValidation(string name);

        Task<int> CreateGame(NameAndBotAmountInputViewModel startInputViewModel);
    }
}
