using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Cards
{
    public interface ICards
    {
        void TakeCard();

        void FirstCardsTaking();

        void RemoveCards();



        bool Equals21();

        bool MoreThan21();
    }
}
