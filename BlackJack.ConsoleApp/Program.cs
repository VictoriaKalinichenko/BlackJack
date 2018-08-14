using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.GameMainClass;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;
using BlackJack.BLL.Helpers;
using BlackJack.DataOnView.PageView;

namespace BlackJack.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWork db = new EFUnitOfWork();
            MainGame Game = new MainGame(db);
            string PlayerName = NameInput(Game);

            int keyForResume = CreateOrResume(PlayerName, Game);
            
            if (keyForResume == 1)
            {
                Game.Resume(PlayerName);
            }
            

            int exit = 1;

            while (exit != 0)
            {
                if (keyForResume == 0)
                {
                    int AmountOfBots = AmountOfBotsInput(Game);
                    Game.Create(PlayerName, AmountOfBots);
                }
                keyForResume = 0;

                while (!Game.IsGameOver())
                {
                    switch(Game.GetStage())
                    {
                        case 0:
                            {
                                RoundStarting(Game);
                                FirstRoundPhase(Game);
                                OneMoreCardAddingToHumanPlayer(Game);
                                SecondRoundPhase(Game);
                                EndRoundBetPayment(Game);
                                break;
                            }
                        case 1:
                            {
                                FirstRoundPhase(Game);
                                OneMoreCardAddingToHumanPlayer(Game);
                                SecondRoundPhase(Game);
                                EndRoundBetPayment(Game);
                                break;
                            }
                        case 2:
                            {
                                OneMoreCardAddingToHumanPlayer(Game);
                                SecondRoundPhase(Game);
                                EndRoundBetPayment(Game);
                                break;
                            }
                        case 3:
                            {
                                SecondRoundPhase(Game);
                                EndRoundBetPayment(Game);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    Game.GameStageUpdate();
                    Game.RoundEnding();
                }
                
                exit = GameEnding(Game);
            }
        }

        #region MainFunctions

        static int CreateOrResume(string name, MainGame Game)
        {
            int key;

            Console.Clear();

            if(Game.PlayerGameExist(name))
            {
                Console.WriteLine(Message.Press1ToResumeGame);
            }
            Console.WriteLine(Message.Press0ToStartNewGame);
            key = KeyInput();

            return key;
        }


        static void RoundStarting(MainGame Game)
        {
            RoundStartPage roundStartPage = Game.GenerateRoundStartPage();
            RoundStartPageOutput(roundStartPage);
            int bet = BetInput(Game);

            Game.GameStageIncrement();
        }

        static void FirstRoundPhase(MainGame Game)
        {
            Game.FirstCardsDistribution();

            RoundProcessPage roundProcessPage = Game.GenerateRoundProcessPage();
            FirstRoundPhaseOutput(roundProcessPage);

            int key = KeyInput();
            Game.BetPayment(key);

            roundProcessPage = Game.GenerateRoundProcessPage();
            FirstRoundPhaseOutput(roundProcessPage);

            Game.GameStageIncrement();
        }

        static void OneMoreCardAddingToHumanPlayer(MainGame Game)
        {
            int key = 1;

            while (Game.CanHumanTakeOneMoreCard() && key == 1)
            {
                RoundProcessPage roundPageForHumanCardsAdding = Game.GenerateRoundProcessPage();
                FirstRoundPhaseOutput(roundPageForHumanCardsAdding);

                Console.WriteLine(Message.Press0ToEnough);
                Console.WriteLine(Message.Press1ToTakeCard);

                key = KeyInput();

                if (key == 1)
                {
                    Game.OneMoreCardAddingToHumanPlayer();

                    roundPageForHumanCardsAdding = Game.GenerateRoundProcessPage();
                    FirstRoundPhaseOutput(roundPageForHumanCardsAdding);
                }
            }

            Game.GameStageIncrement();
        }

        static void SecondRoundPhase(MainGame Game)
        {
            Game.SecondCardsDistributionToBots();

            RoundProcessPage roundProcessPage = Game.GenerateRoundProcessPage();
            SecondRoundPhaseOutput(roundProcessPage);
            Console.WriteLine(Message.PressAnyKeyToContinue);
            Console.ReadKey();
            
            Game.GameStageIncrement();
        }

        static void EndRoundBetPayment(MainGame Game)
        {
            Game.BetPayment();

            RoundProcessPage roundProcessPage = Game.GenerateRoundProcessPage();
            SecondRoundPhaseOutput(roundProcessPage);
            Console.WriteLine(Message.PressAnyKeyToContinue);
            Console.ReadKey();

            if (Game.RemovingBotsWithNullScore())
            {
                roundProcessPage = Game.GenerateRoundProcessPage();
                SecondRoundPhaseOutput(roundProcessPage);
                Console.WriteLine(Message.PressAnyKeyToContinue);
                Console.ReadKey();
            }
        }

        
        static int GameEnding(MainGame Game)
        {
            int exit;

            RoundStartPage roundStartPage = Game.GenerateRoundStartPage();
            RoundStartPageOutput(roundStartPage);

            Console.WriteLine(Message.GameOver);
            Console.WriteLine(Game.GetWinner());
            Console.WriteLine(Message.PressAnyKeyToContinue);
            Console.ReadKey();

            Game.GameDelete();
            exit = StartNewGameOrExit();

            return exit;
        }
        
        #endregion

        #region Input

        static string NameInput(MainGame game)
        {
            string name = "";

            bool correct = false;

            while(!correct)
            {
                Console.Clear();
                Console.WriteLine(Message.InputName);
                try
                {
                    name = Console.ReadLine();
                    game.PlayerNameValidation(name);
                    correct = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(Message.TryAgain);
                    Console.ReadKey();
                }
            }

            return name;
        }

        static int AmountOfBotsInput(MainGame game)
        {
            int AmountOfBots = 0;

            Console.Clear();
            Console.WriteLine(Message.InputAmountOfBots);
            AmountOfBots = Convert.ToInt32(Console.ReadLine());

            return AmountOfBots;
        }

        static int BetInput(MainGame game)
        {
            int bet = 0;

            bool correct = false;

            while (!correct)
            {
                Console.WriteLine(Message.InputBet);
                try
                {
                    bet = Convert.ToInt32(Console.ReadLine());
                    game.BetsCreation(bet);
                    correct = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(Message.TryAgain);
                }
            }

            return bet;

        }


        static int KeyInput()
        {
            int key = 0;

            key = Convert.ToInt32(Console.ReadLine());

            return key;
        }

        #endregion

        #region Output

        static void RoundStartPageOutput(RoundStartPage rsp)
        {
            Console.Clear();

            foreach (PlayerInfo playerInfo in rsp.Players)
            {
                PlayerInfoOutput(playerInfo);
                Console.WriteLine();
            }
        }

        static void FirstRoundPhaseOutput(RoundProcessPage roundProcessPage)
        {
            Console.Clear();

            foreach (PlayerWithCardsInfo player in roundProcessPage.Players)
            {
                bool OnlyFirstCard = true;

                PlayerInfoOutput(player.PlayerInfo);

                if (!(player.PlayerInfo.PlayerType == "Dealer"))
                {
                    Console.WriteLine("Bet: " + player.Bet);
                    Console.WriteLine("CardPoints: " + player.RoundScore);
                    OnlyFirstCard = false;
                }

                CardListOutput(player.Cards, OnlyFirstCard);
                Console.WriteLine();
            }

            foreach (string message in roundProcessPage.Messages)
            {
                Console.WriteLine(message);
            }
        }

        static void SecondRoundPhaseOutput(RoundProcessPage roundProcessPage)
        {
            Console.Clear();

            foreach (PlayerWithCardsInfo player in roundProcessPage.Players)
            {
                PlayerInfoOutput(player.PlayerInfo);

                if (player.PlayerInfo.PlayerType != "Dealer")
                {
                    Console.WriteLine("Bet: " + player.Bet);
                }

                Console.WriteLine("CardPoints: " + player.RoundScore);

                CardListOutput(player.Cards);
                Console.WriteLine();
            }

            foreach (string message in roundProcessPage.Messages)
            {
                Console.WriteLine(message);
            }
        }


        static void PlayerInfoOutput (PlayerInfo playerInfo)
        {
            Console.WriteLine(playerInfo.Name + " (" + playerInfo.PlayerType + ")");
            Console.WriteLine("Score: " + playerInfo.Score);
        }

        static void CardListOutput(List<string> cards, bool OnlyFirstCard = false)
        {
            int CardCount = cards.Count;
            if (OnlyFirstCard)
            {
                CardCount = 1;
            }

            Console.Write("Cards: ");
            for (int i = 0; i < CardCount; i++)
            {
                Console.Write(cards[i]);

                string coma = ", ";
                if (i == CardCount - 1)
                {
                    coma = "";
                }

                Console.Write(coma);
            }

            Console.WriteLine();
        }

        #endregion
        

        static int StartNewGameOrExit()
        {
            int key;

            Console.Clear();
            Console.WriteLine(Message.Press1ToStartNewGame);
            Console.WriteLine(Message.Press0ToExit);
            key = KeyInput();

            return key;
        }
    }
}
