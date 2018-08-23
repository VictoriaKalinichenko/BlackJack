using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Interfaces
{
    public interface IGamePlayerRepository
    {
        List<GamePlayer> GetByGameId(int gameId);

        GamePlayer Get(int id);

        Player GetPlayerByGamePlayerId(int gamePlayerId);

        List<GamePlayer> GetWithPlayersByGameId(int gameId);

        GamePlayer Create(GamePlayer gamePlayer);

        void Update(GamePlayer gamePlayer);

        void Delete(int id);

        void DeleteByGameId(int gameId);
    }
}
