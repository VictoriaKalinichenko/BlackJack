using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.ConsoleApp.ConsoleInputOutput.ConsoleOutputs;

namespace BlackJack.ConsoleApp.ConsoleInputOutput.ConsoleInputs
{
    public class ConsoleInput : IConsoleInput
    {
        IConsoleOutput ConsoleOutput = new ConsoleOutput();

        public string InputName()
        {
            string name = "";

            Console.WriteLine(Message.InputName);
            try
            {
                name = Console.ReadLine();
            }
            catch (Exception ex)
            {
                ConsoleOutput.ExitWithMessage(ex.Message);
            }

            return name;
        }

        public int InputAmountOfBots()
        {
            int amountOfBots = 0;

            try
            {
                Console.Clear();
                Console.WriteLine(Message.InputAmountOfBots);
                amountOfBots = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                ConsoleOutput.ExitWithMessage(ex.Message);
            }

            return amountOfBots;
        }

        public int InputBet()
        {
            int bet = 0;

            try
            {
                Console.WriteLine(Message.InputBet);
                bet = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                ConsoleOutput.ExitWithMessage(ex.Message);
            }
            
            return bet;
        }

        public int InputKeyForBjPayment(bool BjAndDealerBjDanger)
        {
            int key = 0;

            Console.WriteLine(Message.Press0ToContinue);
            if (BjAndDealerBjDanger)
            {
                Console.WriteLine(Message.Press1ToTakeReward);
            }

            try
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                ConsoleOutput.ExitWithMessage(ex.Message);
            }

            return key;
        }

        public int InputKeyForAddingCard()
        {
            int key = 0;
            Console.WriteLine(Message.Press0ToEnough);
            Console.WriteLine(Message.Press1ToTakeCard);  

            try
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                ConsoleOutput.ExitWithMessage(ex.Message);
            }

            return key;
        }
    }
}
