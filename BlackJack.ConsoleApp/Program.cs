using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Providers.Interfaces;
using BlackJack.BLL.Providers;

namespace BlackJack.ConsoleApp
{
    class Program
    {
        public static IMainGameProvider _game;

        static Program()
        {
            _game = new MainGameProvider();
        }

        public static void Main(string[] args)
        {
            _game.Start();

            Console.ReadKey();
        }
    }
}
