using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerCardRepository : IPlayerCardRepository 
    {
        private string _connectionString;


        public PlayerCardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PlayerCard>> GetByGamePlayerId(long gamePlayerId)
        {
            string sqlQuery = $@"SELECT * FROM PlayerCards AS A
                                 INNER JOIN Cards AS B ON A.CardId = B.Id
                                 WHERE A.GamePlayerId = @gamePlayerId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<PlayerCard> playerCards = await db.QueryAsync<PlayerCard, Card, PlayerCard>(sqlQuery, (playerCard, card) =>
                {
                    playerCard.Card = card;
                    return playerCard;
                },
                new { gamePlayerId },
                null);
                return playerCards;
            }
        }

        public async Task<IEnumerable<long>> GetCardsOnHandsIdsByGameId(long gameId)
        {
            string sqlQuery = $@"SELECT CardId FROM PlayerCards AS A
                                 INNER JOIN GamePlayers AS B ON A.GamePlayerId = B.Id
                                 WHERE B.GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<long> cardIds = await db.QueryAsync<long>(sqlQuery, new { gameId });
                return cardIds;
            }
        }

        public async Task<IEnumerable<PlayerCard>> GetPlayerCardsByGameId(long gameId)
        {
            string sqlQuery = $@"SELECT A.Id FROM PlayerCards AS A
                                 INNER JOIN GamePlayers AS B ON A.GamePlayerId = B.Id
                                 WHERE B.GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<PlayerCard> playerCardIds = await db.QueryAsync<PlayerCard>(sqlQuery, new { gameId });
                return playerCardIds;
            }
        }

        public async Task<PlayerCard> Create(PlayerCard playerCard)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                long playerCardId = await db.InsertAsync(playerCard);
                playerCard.Id = playerCardId;
                return playerCard;
            }
        }

        public async Task CreateMany(IEnumerable<PlayerCard> playerCards)
        {
            using (DbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var bulkOperation = new BulkOperation(db);
                bulkOperation.DestinationTableName = "PlayerCards";
                await bulkOperation.BulkInsertAsync(playerCards);
                db.Close();
            }
        }

        public async Task DeleteMany(IEnumerable<PlayerCard> playerCards)
        {
            using (DbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var bulkOperation = new BulkOperation(db);
                bulkOperation.DestinationTableName = "PlayerCards";
                await bulkOperation.BulkDeleteAsync(playerCards);
                db.Close();
            }
        }
    }
}
