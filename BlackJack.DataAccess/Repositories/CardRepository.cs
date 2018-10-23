using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(string connectionString) : base(connectionString)
        { }

        public async Task<IEnumerable<Card>> GetSpecifiedAmount(int cardAmount)
        {
            string sqlQuery = $@"SELECT TOP (@cardAmount) Id, Rank, Lear FROM Cards
                                 ORDER BY NEWID()";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<Card> cards = await db.QueryAsync<Card>(sqlQuery, new { cardAmount });
                return cards;
            }
        }
    }
}
