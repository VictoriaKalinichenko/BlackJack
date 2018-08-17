using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.ViewModels;

namespace BlackJack.ConsoleApp.ConsoleInputOutput.ConsoleOutputs
{
    public class ConsoleOutput : IConsoleOutput
    {
        public void ExitWithMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(Message.AppClosing);
            Console.ReadKey();

            Environment.Exit(0);
        }

        public void RoundStartPageOutput(List<PlayerViewModel> players)
        {
            Console.Clear();

            PlayerViewModel dealer = players.Where(m => m.Player.IsDealer).First();
            PlayerViewModel human = players.Where(m => m.Player.IsHuman).First();
            
            PlayerInfoOutput(dealer, "Dealer");
            PlayerInfoOutput(human, "Human");

            List<PlayerViewModel> bots = players
                .Where(m => m.Player.IsDealer == false)
                .Where(m => m.Player.IsHuman == false)
                .ToList();

            foreach (PlayerViewModel bot in bots)
            {
                PlayerInfoOutput(bot, "Bot");
            }
        }

        public void RoundFirstPhaseOutput(List<PlayerViewModel> players)
        {
            Console.Clear();

            PlayerViewModel dealer = players.Where(m => m.Player.IsDealer).First();
            RoundFirstPhaseDealerInfoOutput(dealer);

            PlayerViewModel human = players.Where(m => m.Player.IsHuman).First();
            RoundPlayerInfoOutput(human, "Human");

            List<PlayerViewModel> bots = players
                .Where(m => m.Player.IsDealer == false)
                .Where(m => m.Player.IsHuman == false)
                .ToList();

            foreach (PlayerViewModel bot in bots)
            {
                RoundPlayerInfoOutput(bot, "Bot");
            }
        }

        public void RoundSecondPhaseOutput(List<PlayerViewModel> players)
        {
            Console.Clear();

            PlayerViewModel dealer = players.Where(m => m.Player.IsDealer).First();
            RoundPlayerInfoOutput(dealer, "Dealer");

            PlayerViewModel human = players.Where(m => m.Player.IsHuman).First();
            RoundPlayerInfoOutput(human, "Human");

            List<PlayerViewModel> bots = players
                .Where(m => m.Player.IsDealer == false)
                .Where(m => m.Player.IsHuman == false)
                .ToList();

            foreach (PlayerViewModel bot in bots)
            {
                RoundPlayerInfoOutput(bot, "Bot");
            }
        }



        private void PlayerInfoOutput(PlayerViewModel player, string playerType)
        {
            Console.WriteLine(player.Player.Name + " (" + playerType + ")");
            Console.WriteLine("Score: " + player.GameScore.Score);
            Console.WriteLine();
        }
        
        private void RoundPlayerInfoOutput (PlayerViewModel player, string playerType)
        {
            Console.WriteLine(player.Player.Name + " (" + playerType + ")");
            Console.WriteLine("Score: " + player.GameScore.Score);

            if (!player.Player.IsDealer)
            {
                Console.WriteLine("Bet: " + player.GameScore.Bet);
            }

            Console.WriteLine("Card score: " + player.GameScore.RoundScore);
            CardListOutput(player.Cards);
            Console.WriteLine();
        }

        private void RoundFirstPhaseDealerInfoOutput(PlayerViewModel dealer)
        {
            Console.WriteLine(dealer.Player.Name + " (Dealer)");
            Console.WriteLine("Score: " + dealer.GameScore.Score);
            Console.WriteLine("First card: " + dealer.Cards[0].ToString());
            Console.WriteLine();
        }

        private void CardListOutput(List<Card> cards)
        {
            Console.Write("Cards: ");
            for (int i = 0; i < cards.Count; i++)
            {
                Console.Write(cards[i].ToString());

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
