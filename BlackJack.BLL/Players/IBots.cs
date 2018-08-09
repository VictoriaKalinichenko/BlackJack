using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.Players
{
    public interface IBots
    {
        void CreateBets();

        void FirstCardsAdding();

        void BJChecking(bool DealerBJDanger);

        void SecondCardsEdding();

        void SecondCardsChecking();

        void RemoveCards();


        bool IsRoundOver();

        List<Player> GetBots();
    }
}
