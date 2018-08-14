using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Providers.GamePlayerProviders
{
    public interface IGamePlayerProvider
    {
        void Create(int playerId, int gameId);

        void DeletePlayerRelations(int playerId);

        void DeleteGameRelations(int gameId);
    }
}
