using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.ViewModels;

namespace BlackJack.BLL.Providers.Interfaces
{
    public interface ICardCheckProvider
    {
        bool FirstCardCheck(List<PlayerViewModel> players);


    }
}
