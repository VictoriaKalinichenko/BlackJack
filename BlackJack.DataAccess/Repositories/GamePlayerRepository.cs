using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    public class GamePlayerRepository : BaseRepository<GamePlayer>, IGamePlayerRepository
    {
        public GamePlayerRepository(string connectionString) : base(connectionString)
        { }

        public async Task<IEnumerable<GamePlayer>> GetAllForInitializeRound(long gameId)
        {
            string sqlQuery = $@"SELECT A.Id, Score, B.Id, B.Name, B.Type
                                 FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<GamePlayer> gamePlayers = 
                    await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery, (gamePlayer, player) =>
                {
                    gamePlayer.Player = player;
                    return gamePlayer;
                },
                new { gameId = gameId },
                null);
                return gamePlayers;
            }
        }
        
        public async Task<IEnumerable<GamePlayer>> GetAllWithoutCards(long gameId)
        {
            string sqlQuery = $@"SELECT A.Id, A.GameId, Score, RoundScore, 
                                 BetPayCoefficient, Bet, CardAmount, B.Id, B.Name, B.Type
                                 FROM GamePlayers AS A 
                                 INNER JOIN Players AS B ON A.PlayerId = B.Id
                                 WHERE A.GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<GamePlayer> gamePlayers = await db.QueryAsync<GamePlayer, Player, GamePlayer>(sqlQuery, (gamePlayer, player) =>
                {
                    gamePlayer.Player = player;
                    return gamePlayer;
                },
                new { gameId = gameId },
                null);
                return gamePlayers;
            }
        }

        public async Task<GamePlayer> GetWithCards(long gameId, int playerType)
        {
            string sqlQuery = $@"SELECT A.Id, A.GameId, A.RoundScore, A.CardAmount, A.Score, A.Bet, 
                                 A.BetPayCoefficient, B.Id, C.Id, C.Rank, C.Lear, D.Id, D.Name, D.Type
                                 FROM GamePlayers AS A 
                                 LEFT JOIN PlayerCards AS B ON A.Id = B.GamePlayerId
                                 LEFT JOIN Cards AS C ON B.CardId = C.Id
                                 LEFT JOIN Players AS D ON A.PlayerId = D.Id
                                 WHERE A.GameId = @gameId AND D.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var gamePlayers = new List<GamePlayer>();
                await db.QueryAsync<GamePlayer, PlayerCard, Card, Player, GamePlayer>(sqlQuery, (gamePlayer, playerCard, card, player) =>
                {
                    gamePlayer.Player = player;
                    if (gamePlayers.Find(m => m.Id == gamePlayer.Id) == null)
                    {
                        gamePlayers.Add(gamePlayer);
                    }
                    
                    if (gamePlayers.Find(m => m.Id == gamePlayer.Id).PlayerCards == null)
                    {
                        gamePlayers.Find(m => m.Id == gamePlayer.Id).PlayerCards = new List<PlayerCard>();
                    }

                    playerCard.Card = card;
                    gamePlayers.Find(m => m.Id == gamePlayer.Id).PlayerCards.Add(playerCard);
                    return gamePlayer;
                },
                new { gameId = gameId, playerType = playerType },
                null);

                return gamePlayers.First();
            }
        }
       
        public async Task<IEnumerable<GamePlayer>> GetAllWithCards(long gameId)
        {
            string sqlQuery = $@"SELECT A.Id, A.GameId, A.RoundScore, A.Score, A.Bet, A.CardAmount, A.BetPayCoefficient, 
                                 B.Id, C.Id, C.Rank, C.Lear, D.Id, D.Name, D.Type
                                 FROM GamePlayers AS A 
                                 LEFT JOIN PlayerCards AS B ON A.Id = B.GamePlayerId
                                 LEFT JOIN Cards AS C ON B.CardId = C.Id
                                 LEFT JOIN Players AS D ON A.PlayerId = D.Id
                                 WHERE A.GameId = @gameId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var gamePlayers = new List<GamePlayer>();
                await db.QueryAsync<GamePlayer, PlayerCard, Card, Player, GamePlayer>(sqlQuery, (gamePlayer, playerCard, card, player) =>
                {
                    gamePlayer.Player = player;
                    if (gamePlayers.Find(m => m.Id == gamePlayer.Id) == null)
                    {
                        gamePlayers.Add(gamePlayer);
                    }
                    
                    if (gamePlayers.Find(m => m.Id == gamePlayer.Id).PlayerCards == null)
                    {
                        gamePlayers.Find(m => m.Id == gamePlayer.Id).PlayerCards = new List<PlayerCard>();
                    }

                    playerCard.Card = card;
                    gamePlayers.Find(m => m.Id == gamePlayer.Id).PlayerCards.Add(playerCard);
                    return gamePlayer;
                },
                new { gameId = gameId },
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

        public async Task UpdateMany(IEnumerable<GamePlayer> gamePlayers)
        {
            string sqlQuery = @"UPDATE GamePlayers 
                                SET Score = @Score, RoundScore = @RoundScore, CardAmount = @CardAmount, 
                                BetPayCoefficient = @BetPayCoefficient, Bet = @Bet
                                WHERE Id = @Id";

            using (DbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sqlQuery, gamePlayers.ToArray());
            }
        }

        public async Task UpdateAddingCard(GamePlayer gamePlayer)
        {
            string sqlQuery = @"UPDATE GamePlayers SET RoundScore = @roundScore, CardAmount = @cardAmount
                                WHERE Id = @gamePlayerId";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(sqlQuery, 
                    new { roundScore = gamePlayer.RoundScore, cardAmount = gamePlayer.CardAmount, gamePlayerId = gamePlayer.Id });
            }
        }

        public async Task UpdateManyAfterContinueRound(IEnumerable<GamePlayer> gamePlayers)
        {
            string sqlQuery = @"UPDATE GamePlayers 
                                SET RoundScore = @RoundScore, CardAmount = @CardAmount, BetPayCoefficient = @BetPayCoefficient 
                                WHERE Id = @Id";

            using (DbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sqlQuery, gamePlayers.ToArray());
            }
        }
        
        public async Task DeleteBotsWithZeroScore(long gameId)
        {
            string sqlQuery = @"DELETE FROM GamePlayers
                                WHERE Id IN (SELECT A.Id FROM GamePlayers AS A
                                INNER JOIN Players AS B ON A.PlayerId = B.Id
                                WHERE A.GameId = @gameId AND A.Score <= 0 AND B.Type = @playerType)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.QueryAsync(sqlQuery, new { gameId = gameId, playerType = PlayerType.Bot });
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
