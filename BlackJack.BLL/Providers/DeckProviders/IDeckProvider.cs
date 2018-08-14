using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Helpers;

namespace BlackJack.BLL.Providers.DeckProviders
{
    public interface IDeckProvider
    {
        List<Card> CreateDeck();

        Card TakeCardFromDeck(List<Card> cards);

        List<Card> ShuffleDeck(List<Card> cards);
    }
}
