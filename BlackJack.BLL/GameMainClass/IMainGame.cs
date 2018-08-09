using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Players;

namespace BlackJack.BLL.GameMainClass
{
    public interface IMainGame
    {
        IPlayer HumanPlayer { get; }

        IDealer Dealer { get; }

        IBots Bots { get; }



        void Create(string name, int AmountOfBots);

        void Resume(string name);

        void FirstCardsAdding();

        void RemoveAllCards();

        void RoundEnding();

        void GameEnding();


        bool IsGameOver();

        bool IsRoundOver();
    }
}
