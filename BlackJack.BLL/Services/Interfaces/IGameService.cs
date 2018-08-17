using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity.Models;
using BlackJack.ViewModels.ViewModels;
using BlackJack.BLL.Helpers;

namespace BlackJack.BLL.Services.Interfaces
{
    public interface IGameService
    {
        string PlayerNameValidation(string name);

        void DeletePlayer(int playerId);


        GameViewModel CreateGame(string name, int amountOfBots);

        void UpdateGameStage(Game game);

        void UpdatePlayerCards(GamePlayer gamePlayer, List<int> cardIds);

        void DeleteGame(int gameId);
    }
}
