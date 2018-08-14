using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.Providers.PlayerProviders
{
    public interface IPlayerProvider
    {
        Player CreateInDb(bool IsDealer = false, bool IsHuman = false, string Name = "");

        Player GetHumanFromList(List<Player> players);

        void DeleteInDb(int PlayerId);
    }
}
