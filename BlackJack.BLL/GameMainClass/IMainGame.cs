using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;
using BlackJack.DataOnView.PageView;

namespace BlackJack.BLL.GameMainClass
{
    public interface IMainGame
    {
        void PlayerNameValidation(string name);

        bool PlayerGameExist(string name);

        void Create(string name, int AmountOfBots);

        void Resume(string humanPlayerName);


        RoundStartPage GenerateRoundStartPage();

        void BetsCreation(int HumanBet);

        void FirstCardsDistribution();


        bool CanHumanTakeOneMoreCard();

        void OneMoreCardAddingToHumanPlayer();

        void SecondCardsDistributionToBots();


        void RoundEnding();

        bool RemovingBotsWithNullScore();


        void GameDelete();

        bool IsGameOver();

        string GetWinner();


        void GameStageIncrement();

        void GameStageUpdate();

        int GetStage();


        void BetPayment(int key = 0);

        RoundProcessPage GenerateRoundProcessPage();
    }
}
