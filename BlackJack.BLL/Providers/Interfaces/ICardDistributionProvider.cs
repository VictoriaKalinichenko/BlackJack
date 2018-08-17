using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BLL.Providers.Interfaces
{
    public interface ICardDistributionProvider
    {
        List<Card> CreateDeck();

        int CardToIntConverter(Card card);

        List<Card> ShuffleDeck(List<Card> cards);
    }
}
