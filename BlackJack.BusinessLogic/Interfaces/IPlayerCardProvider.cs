using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IPlayerCardProvider
    {
        string ConvertCardToString(Card card);

        List<string> GetCardsStringList(IEnumerable<PlayerCard> playerCards);

        Task<Card> AddingCardToPlayer(int gamePlayerId, List<Card> deck);

        int CardScoreCount(IEnumerable<PlayerCard> playerCards);
    }
}
