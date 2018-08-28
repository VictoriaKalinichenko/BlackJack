using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IPlayerCardProvider
    {
        string PlayerCardToCardString(PlayerCard playerCard);

        List<string> GetCardsStringList(IEnumerable<PlayerCard> playerCards);

        Task AddingCardToPlayer(int gamePlayerId, List<int> deck);

        int CardScoreCount(IEnumerable<PlayerCard> playerCards);
    }
}
