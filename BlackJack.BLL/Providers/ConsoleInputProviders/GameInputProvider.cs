using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.Providers.ValidationProviders.Name;
using BlackJack.BLL.Providers.ValidationProviders.Bet;
using BlackJack.Entity;

namespace BlackJack.BLL.Providers.ConsoleInputProviders
{
    public class GameInputProvider : IGameInputProvider
    {
        public string InputName()
        {
            string name;
            
            INameValidationProvider nameValidation = new NameValidationProvider();
            
            Console.WriteLine(Message.InputName);
            try
            {
                name = Console.ReadLine();
                nameValidation.Validate(name);
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(Message.TryAgain);
                name = InputName();
            }

            return name;
        }

        public int InputAmountOfBots()
        {
            int amountOfBots = 0;

            Console.Clear();
            Console.WriteLine(Message.InputAmountOfBots);
            amountOfBots = Convert.ToInt32(Console.ReadLine());

            return amountOfBots;
        }

        public int InputBet(Player player)
        {
            int bet = 0;

            IBetValidationProvider betValidation = new BetValidationProvider();
            
            Console.WriteLine(Message.InputBet);
            try
            {
                bet = Convert.ToInt32(Console.ReadLine());
                betValidation.Validate(player, bet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(Message.TryAgain);
                bet = InputBet(player);
            }

            return bet;
        }
    }
}
