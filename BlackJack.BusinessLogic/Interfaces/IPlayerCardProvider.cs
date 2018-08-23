using System.Collections.Generic;
using BlackJack.Entities.Models;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IPlayerCardProvider
    {
        string PlayerCardToCardString(PlayerCard playerCard);

        List<string> GetCardsStringList(List<PlayerCard> playerCards);

        void AddingCardToPlayer(int gamePlayerId, List<int> deck);

        int CardScoreCount(List<PlayerCard> playerCards);
    }
}
