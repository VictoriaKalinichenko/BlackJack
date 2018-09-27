﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerCardRepository
    {
        Task<IEnumerable<PlayerCard>> GetByGamePlayerId(long gamePlayerId);
        
        Task<IEnumerable<Card>> GetCardsOnHands(long gameId);

        Task<IEnumerable<PlayerCard>> GetAllByGameId(long gameId);
        
        Task<PlayerCard> Create(PlayerCard obj);

        Task CreateMany(IEnumerable<PlayerCard> playerCards);

        Task DeleteMany(IEnumerable<PlayerCard> playerCards);
    }
}
