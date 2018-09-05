using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Models;
using Dapper;

namespace BlackJack.DataAccess.Repositories
{
    public class CardRepository : ICardRepository
    {
        private string _connectionString;


        public CardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Card>> GetAll()
        {
            string sqlQuery = $@"SELECT Id, CardName, CardType
                                 FROM Cards";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var cards = await db.QueryAsync<Card>(sqlQuery);
                return cards;
            }
        }
    }
}
