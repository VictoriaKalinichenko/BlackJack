using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.Randomize
{
    public interface IRandomize
    {
        string NameGenerate();

        int BetGenerate();

        int CardIdSelection(int AmountOfCard);
    }
}
