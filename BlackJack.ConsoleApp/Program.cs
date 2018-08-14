using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.GameMainClass;

namespace BlackJack.ConsoleApp
{
    class Program
    {
        public static MainGame _game;

        static Program()
        {
            _game = new MainGame();
        }

        public static void Main(string[] args)
        {
            _game.Start();

            Console.ReadKey();
        }
    }
}
