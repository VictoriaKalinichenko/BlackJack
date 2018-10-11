using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        public CardRepository(string connectionString) : base(connectionString)
        { }

        public async Task<IEnumerable<Card>> ResumeDeck(long gameId)
        {
            string sqlQuery = $@"SELECT Id, Name, Type FROM Cards
                                 EXCEPT
                                 SELECT C.Id, C.Name, C.Type
                                 FROM PlayerCards AS A
                                 INNER JOIN GamePlayers AS B ON A.GamePlayerId = B.Id
                                 INNER JOIN Cards As C ON C.Id = A.CardId
                                 WHERE B.GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<Card> cards = await db.QueryAsync<Card>(sqlQuery, new { gameId });
                return cards;
            }
        }
    }
}
