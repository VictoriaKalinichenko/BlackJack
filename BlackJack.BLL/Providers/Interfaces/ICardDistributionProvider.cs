using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.BLL.ViewModels;

namespace BlackJack.BLL.Providers.Interfaces
{
    public interface ICardDistributionProvider
    {
        List<Card> CreateDeck();

        void FirstCardsDistribution(List<PlayerViewModel> players, List<Card> deck);

        bool OneMoreCardToHuman(PlayerViewModel player, List<Card> deck = null, int takeCardKey = 0);
    }
}
