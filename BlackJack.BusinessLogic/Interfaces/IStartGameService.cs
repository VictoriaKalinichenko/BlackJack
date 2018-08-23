using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IStartGameService
    {
        string PlayerNameValidation(string name);

        int CreateGame(NameAndBotAmountInputViewModel startInputViewModel);
    }
}
