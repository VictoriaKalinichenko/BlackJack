using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.BLL.Players;

namespace BlackJack.BLL.GameMainClass
{
    public interface IGame
    {
        BjPlayer HumanPlayer { get; }

        BjDealer Dealer { get; }

        BjBots Bots { get; }



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
