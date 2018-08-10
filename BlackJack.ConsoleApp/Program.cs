using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.GameMainClass;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;
using BlackJack.BLL.Helpers;
using BlackJack.DataOnView;
using BlackJack.DataOnView.RoundFirstPhase;

namespace BlackJack.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWork db = new EFUnitOfWork();
            MainGame Game = new MainGame(db);

            string PlayerName = NameInput(Game);
            int AmountOfBots = AmountOfBotsInput(Game);

            Game.Create(PlayerName, AmountOfBots);

            RoundStartPage roundStartPage = Game.GenerateRoundStartPage();
            RoundStartPageOutput(roundStartPage);
            int bet = BetInput(Game);

            Game.FirstCardsDistribution();

            RoundFirstPhasePage roundFirstPhasePage = Game.GenerateRoundFirstPhasePage();
            RoundFirstPhasePageOutput(roundFirstPhasePage);

            Console.ReadKey();
        }

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

        static void RoundStartPageOutput(RoundStartPage rsp)
        {
            Console.Clear();

            foreach (PlayerInfo playerInfo in rsp.Players)
            {
                PlayerInfoOutput(playerInfo);
                Console.WriteLine();
            }
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

        static void RoundFirstPhasePageOutput(RoundFirstPhasePage roundFirstPhasePage)
        {
            Console.Clear();

            foreach (PlayerRoundFirstPhaseInfo player in roundFirstPhasePage.Players)
            {
                PlayerInfoOutput(player.PlayerInfo);

                if (!(player.PlayerInfo.PlayerType == "Dealer"))
                {
                    Console.WriteLine("Bet: " + player.Bet);
                }

                Console.WriteLine("CardPoints: " + player.RoundScore);
                CardListOutput(player.Cards);
                Console.WriteLine();
            }
        }



        static void PlayerInfoOutput (PlayerInfo playerInfo)
        {
            Console.WriteLine(playerInfo.Name + " (" + playerInfo.PlayerType + ")");
            Console.WriteLine("Score: " + playerInfo.Score);
        }

        static void CardListOutput(List<string> cards)
        {
            Console.Write("Cards: ");

            for (int i = 0; i < cards.Count; i++)
            {
                Console.Write(cards[i]);

                string coma = ", ";
                if (i == cards.Count - 1)
                {
                    coma = "";
                }

                Console.Write(coma);
            }

            Console.WriteLine();
        }
    }
}
