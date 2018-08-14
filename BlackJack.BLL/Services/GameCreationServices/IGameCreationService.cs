using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Helpers;

namespace BlackJack.BLL.Services.GameCreationServices
{
    public interface IGameCreationService
    {
        Game CreateGame();

        List<Player> CreatePlayerList(string humanName, int amountOfBots, int gameId);

        List<Card> CreateDeck();
    }
}
