using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerCardRepository : IPlayerCardRepository 
    {
        private string _connectionString;


        public PlayerCardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PlayerCard>> GetByGamePlayerId(int gamePlayerId)
        {
            string sqlQuery = $@"SELECT * FROM PlayerCards AS A
                                 INNER JOIN Cards AS B ON A.CardId = B.Id
                                 WHERE A.GamePlayerId = @gamePlayerId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var playerCards = await db.QueryAsync<PlayerCard, Card, PlayerCard>(sqlQuery, (playerCard, card) =>
                {
                    playerCard.Card = card;
                    return playerCard;
                },
                new { gamePlayerId },
                null);
                return playerCards;
            }
        }

        public async Task<int> GetCountByGamePlayerId(int gamePlayerId)
        {
            string sqlQuery = $@"SELECT COUNT(GamePlayerId) FROM PlayerCards 
                                 WHERE GamePlayerId = @gamePlayerId
                                 GROUP BY GamePlayerId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int playerCardsCount = await db.QuerySingleOrDefaultAsync<int>(sqlQuery, new { gamePlayerId });
                return playerCardsCount;
            }
        }

        public async Task<PlayerCard> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                PlayerCard playerCard = await db.GetAsync<PlayerCard>(id);
                return playerCard;
            }
        }

        public async Task<PlayerCard> Create(PlayerCard playerCard)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int playerCardId = await db.InsertAsync(playerCard);
                playerCard.Id = playerCardId;
                return playerCard;
            }
        }

        public async Task DeleteByGamePlayerId(int gamePlayerId)
        {
            string sqlQuery = $@"DELETE FROM PlayerCards 
                                 WHERE GamePlayerId = @gamePlayerId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { gamePlayerId });
            }
        }
    }
}
