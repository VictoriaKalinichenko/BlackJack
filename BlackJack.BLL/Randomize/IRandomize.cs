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
        int BetGenerate(int PlayerScore);

        int CardIdSelection(int AmountOfCard);
    }
}
