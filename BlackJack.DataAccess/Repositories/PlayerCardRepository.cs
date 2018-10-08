using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerCardRepository : GenericRepository<PlayerCard>, IPlayerCardRepository 
    {
        public PlayerCardRepository(string connectionString) : base(connectionString)
        { }

        public async Task<IEnumerable<PlayerCard>> GetByGamePlayerId(long gamePlayerId)
        {
            string sqlQuery = $@"SELECT * FROM PlayerCards AS A
                                 INNER JOIN Cards AS B ON A.CardId = B.Id
                                 WHERE A.GamePlayerId = @gamePlayerId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<PlayerCard> playerCards = 
                    await db.QueryAsync<PlayerCard, Card, PlayerCard>(sqlQuery, (playerCard, card) =>
                {
                    playerCard.Card = card;
                    return playerCard;
                },
                new { gamePlayerId },
                null);
                return playerCards;
            }
        }

        public async Task<IEnumerable<Card>> GetCardsOnHands(long gameId)
        {
            string sqlQuery = $@"SELECT C.Id, C.Name, C.Type
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

        public async Task<IEnumerable<PlayerCard>> GetAllByGameId(long gameId)
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
    }
}
