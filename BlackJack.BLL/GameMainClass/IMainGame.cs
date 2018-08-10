using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.GameMainClass
{
    public interface IMainGame
    {
        void PlayerNameValidation(string name);

        void Create(string name, int AmountOfBots);
    }
}
