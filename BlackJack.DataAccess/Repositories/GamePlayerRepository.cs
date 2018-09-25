using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using Dapper;
using Z.BulkOperations;

namespace BlackJack.DataAccess.Repositories
{
    public class GamePlayerRepository : IGamePlayerRepository
    {
        private string _connectionString;


        public GamePlayerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<GamePlayer> GetSpecificPlayerForStartRound(long gameId, int playerType)
        {
            string sqlQuery = $@"SELECT A.Id, Score, B.Id, B.Name
                                 FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = @gameId AND B.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<GamePlayer> gamePlayers = await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery, (gamePlayer, player) =>
                {
                    gamePlayer.Player = player;
                    return gamePlayer;
                },
                new { gameId = gameId, playerType = playerType }
                );
                return gamePlayers.First();
            }
        }

        public async Task<IEnumerable<GamePlayer>> GetSpecificPlayersForStartRound(long gameId, int playerType)
        {
            string sqlQuery = $@"SELECT A.Id, Score, B.Id, B.Name
                                 FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = @gameId AND B.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<GamePlayer> gamePlayers = await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery, (gamePlayer, player) =>
                {
                    gamePlayer.Player = player;
                    return gamePlayer;
                },
                new { gameId = gameId, playerType = playerType },
                null);
                return gamePlayers;
            }
        }

        public async Task<GamePlayer> GetSpecificPlayerWithoutCards(long gameId, int playerType)
        {
            string sqlQuery = $@"SELECT A.Id, A.PlayerId, Score, RoundScore, BetPayCoefficient, Bet, CardAmount, B.Id, B.Name, B.Type
                                 FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = @gameId AND B.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<GamePlayer> gamePlayers = await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery, (gamePlayer, player) =>
                {
                    gamePlayer.Player = player;
                    return gamePlayer;
                },
                new { gameId = gameId, playerType = playerType },
                null);
                return gamePlayers.First();
            }
        }

        public async Task<IEnumerable<GamePlayer>> GetSpecificPlayersWithoutCards(long gameId, int playerType)
        {
            string sqlQuery = $@"SELECT A.Id, A.PlayerId, Score, RoundScore, BetPayCoefficient, Bet, CardAmount, B.Id, B.Name, B.Type
                                 FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = @gameId AND B.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<GamePlayer> gamePlayers = await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery, (gamePlayer, player) =>
                {
                    gamePlayer.Player = player;
                    return gamePlayer;
                },
                new { gameId = gameId, playerType = playerType },
                null);
                return gamePlayers;
            }
        }

        public async Task<GamePlayer> GetSpecificPlayerWithCards(long gameId, int playerType)
        {
            string sqlQuery = $@"SELECT A.Id, A.RoundScore, A.CardAmount, A.Score, A.Bet, A.BetPayCoefficient, B.Id, C.Id, C.Name, C.Type, D.Id, D.Name, D.Type
                                 FROM GamePlayers AS A 
                                 LEFT JOIN PlayerCards AS B ON A.Id = B.GamePlayerId
                                 LEFT JOIN Cards AS C ON B.CardId = C.Id
                                 INNER JOIN Players AS D ON A.PlayerId = D.Id
                                 WHERE A.GameId = @gameId AND D.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var gamePlayers = new List<GamePlayer>();
                await db.QueryAsync<GamePlayer, PlayerCard, Card, Player, GamePlayer>(sqlQuery, (gamePlayer, playerCard, card, player) =>
                {
                    gamePlayer.Player = player;
                    if (gamePlayers.AsList().FindLast(m => m.Id == gamePlayer.Id) == null)
                    {
                        gamePlayers.AsList().Add(gamePlayer);
                    }

                    playerCard.Card = card;

                    if (gamePlayers.AsList().Find(m => m.Id == gamePlayer.Id).PlayerCards == null)
                    {
                        gamePlayers.AsList().Find(m => m.Id == gamePlayer.Id).PlayerCards = new List<PlayerCard>();
                    }

                    gamePlayers.AsList().Find(m => m.Id == gamePlayer.Id).PlayerCards.Add(playerCard);
                    return gamePlayer;
                },
                new { gameId = gameId, playerType = playerType },
                null);

                return gamePlayers.First();
            }
        }

        public async Task<IEnumerable<GamePlayer>> GetSpecificPlayersWithCards(long gameId, int playerType)
        {
            string sqlQuery = $@"SELECT A.Id, A.RoundScore, A.Score, A.Bet, A.CardAmount, A.BetPayCoefficient, B.Id, C.Id, C.Name, C.Type, D.Id, D.Name, D.Type
                                 FROM GamePlayers AS A 
                                 LEFT JOIN PlayerCards AS B ON A.Id = B.GamePlayerId
                                 LEFT JOIN Cards AS C ON B.CardId = C.Id
                                 INNER JOIN Players AS D ON A.PlayerId = D.Id
                                 WHERE A.GameId = @gameId AND D.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var gamePlayers = new List<GamePlayer>();
                await db.QueryAsync<GamePlayer, PlayerCard, Card, Player, GamePlayer>(sqlQuery, (gamePlayer, playerCard, card, player) =>
                {
                    gamePlayer.Player = player;
                    if (gamePlayers.AsList().FindLast(m => m.Id == gamePlayer.Id) == null)
                    {
                        gamePlayers.AsList().Add(gamePlayer);
                    }

                    playerCard.Card = card;

                    if (gamePlayers.AsList().Find(m => m.Id == gamePlayer.Id).PlayerCards == null)
                    {
                        gamePlayers.AsList().Find(m => m.Id == gamePlayer.Id).PlayerCards = new List<PlayerCard>();
                    }

                    gamePlayers.AsList().Find(m => m.Id == gamePlayer.Id).PlayerCards.Add(playerCard);
                    return gamePlayer;
                },
                new { gameId = gameId, playerType = playerType },
                null);

                return gamePlayers;
            }
        }
        
        public async Task<int> GetScoreById(long gamePlayerId)
        {
            string sqlQuery = $@"SELECT Score FROM GamePlayers
                                 WHERE Id = @gamePlayerId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int score = await db.QueryFirstOrDefaultAsync<int>(sqlQuery, new { gamePlayerId });
                return score;
            }
        }

        public async Task CreateMany(IEnumerable<GamePlayer> gamePlayers)
        {
            using (DbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var bulkOperation = new BulkOperation(db);
                bulkOperation.DestinationTableName = "GamePlayers";
                await bulkOperation.BulkInsertAsync(gamePlayers);
                db.Close();
            }
        }

        public async Task UpdateMany(IEnumerable<GamePlayer> gamePlayers)
        {
            using (DbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var bulkOperation = new BulkOperation(db);
                bulkOperation.DestinationTableName = "GamePlayers";
                bulkOperation.ColumnMappings.Add(new ColumnMapping("Id", true));
                bulkOperation.ColumnMappings.Add(new ColumnMapping("Score", "Score"));
                bulkOperation.ColumnMappings.Add(new ColumnMapping("RoundScore", "RoundScore"));
                bulkOperation.ColumnMappings.Add(new ColumnMapping("CardAmount", "CardAmount"));
                bulkOperation.ColumnMappings.Add(new ColumnMapping("Bet", "Bet"));
                bulkOperation.ColumnMappings.Add(new ColumnMapping("BetPayCoefficient", "BetPayCoefficient"));
                await bulkOperation.BulkMergeAsync(gamePlayers);
                db.Close();
            }
        }

        public async Task UpdateAfterAddingOneMoreCard(GamePlayer gamePlayer)
        {
            string sqlQuery = @"UPDATE GamePlayers SET RoundScore = @roundScore, CardAmount = @cardAmount
                                WHERE Id = @gamePlayerId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(sqlQuery, new { roundScore = gamePlayer.RoundScore, cardAmount = gamePlayer.CardAmount, gamePlayerId = gamePlayer.Id });
            }
        }
        
        public async Task UpdateManyAfterRoundSecondPhase(IEnumerable<GamePlayer> gamePlayers)
        {
            using (DbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var bulkOperation = new BulkOperation(db);
                bulkOperation.DestinationTableName = "GamePlayers";
                bulkOperation.ColumnMappings.Add(new ColumnMapping("Id", true));
                bulkOperation.ColumnMappings.Add(new ColumnMapping("RoundScore", "RoundScore"));
                bulkOperation.ColumnMappings.Add(new ColumnMapping("CardAmount", "CardAmount"));
                bulkOperation.ColumnMappings.Add(new ColumnMapping("BetPayCoefficient", "BetPayCoefficient"));
                await bulkOperation.BulkMergeAsync(gamePlayers);
                db.Close();
            }
        }
        
        public async Task DeleteBotsWithZeroScore(long gameId)
        {
            string sqlQuery = @"DELETE FROM GamePlayers
                                WHERE Score <= 0 AND GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(sqlQuery, new { gameId });
            }
        }

        public async Task DeleteAllByGameId(long gameId)
        {
            string sqlQuery = @"DELETE FROM GamePlayers
                                WHERE GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(sqlQuery, new { gameId });
            }
        }
    }
}
