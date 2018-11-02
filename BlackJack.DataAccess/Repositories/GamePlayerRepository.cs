using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories
{
    public class GamePlayerRepository : BaseRepository<GamePlayer>, IGamePlayerRepository
    {
        public GamePlayerRepository(string connectionString) : base(connectionString)
        { }
                
        public async Task<GamePlayer> GetHumanByGameId(long gameId)
        {
            string sqlQuery = $@"SELECT A.Id, A.GameId,
                                 B.Id, C.Id, C.Rank, C.Lear, C.Worth, D.Id, D.Name, D.Type
                                 FROM GamePlayers AS A 
                                 INNER JOIN PlayerCards AS B ON A.Id = B.GamePlayerId
                                 INNER JOIN Cards AS C ON B.CardId = C.Id
                                 INNER JOIN Players AS D ON A.PlayerId = D.Id
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
                new { gameId = gameId, playerType = PlayerType.Human },
                null);

                return gamePlayers.First();
            }
        }
       
        public async Task<List<GamePlayer>> GetAllByGameId(long gameId)
        {
            string sqlQuery = $@"SELECT A.Id, A.GameId, A.PlayerId,
                                 B.Id, C.Id, C.Rank, C.Lear, C.Worth, D.Id, D.Name, D.Type
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

                    if (playerCard != null && card != null)
                    {
                        playerCard.Card = card;
                        gamePlayers.Find(m => m.Id == gamePlayer.Id).PlayerCards.Add(playerCard);
                    }
                    
                    return gamePlayer;
                },
                new { gameId = gameId },
                null);

                return gamePlayers;
            }
        }
    }
}
