using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.Configuration;
using Dapper;

namespace BlackJack.DataAccess.Repositories
{
    public class GamePlayerRepository : IGamePlayerRepository
    {
        public List<GamePlayer> GetByGameId(int gameId)
        {
            List<GamePlayer> gamePlayers = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                string sqlQuery = "SELECT * FROM GamePlayers WHERE GameId = @gameId";
                gamePlayers = db.Query<GamePlayer>(sqlQuery, new { gameId }).ToList();
            }
            return gamePlayers;
        }

        public List<GamePlayer> GetWithPlayersByGameId(int gameId)
        {
            List<GamePlayer> gamePlayers = null;
            gamePlayers = GetByGameId(gameId);

            foreach(GamePlayer gamePlayer in gamePlayers)
            {
                gamePlayer.Player = GetPlayerByGamePlayerId(gamePlayer.Id);
            }

            return gamePlayers;
        }

        public GamePlayer Get(int id)
        {
            GamePlayer gamePlayer = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                gamePlayer = db.Query<GamePlayer>("SELECT * FROM GamePlayers WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return gamePlayer;
        }

        public Player GetPlayerByGamePlayerId (int gamePlayerId)
        {
            Player player = new Player();
            GamePlayer gamePlayer = Get(gamePlayerId);
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                player = db.Query<Player>("SELECT * FROM Players WHERE Id = @id", new { id = gamePlayer.PlayerId }).FirstOrDefault();
            }
            return player;
        }

        public GamePlayer Create(GamePlayer gamePlayer)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                string sqlQuery = "INSERT INTO GamePlayers (PlayerId, GameId, Score, BetPayCoefficient, Bet, RoundScore) VALUES(@PlayerId, @GameId, @Score, @BetPayCoefficient, @Bet, @RoundScore); SELECT CAST(SCOPE_IDENTITY() as int)";
                int gamePlayerId = db.Query<int>(sqlQuery, gamePlayer).FirstOrDefault();
                gamePlayer.Id = gamePlayerId;
            }
            return gamePlayer;
        }

        public void Update(GamePlayer gamePlayer)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                var sqlQuery = "UPDATE GamePlayers SET Score = @Score, Bet = @Bet, BetPayCoefficient = @BetPayCoefficient, RoundScore = @RoundScore WHERE Id = @Id";
                db.Execute(sqlQuery, gamePlayer);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                var sqlQuery = "DELETE FROM GamePlayers WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public void DeleteByGameId(int gameId)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                var sqlQuery = "DELETE FROM GamePlayers WHERE GameId = @gameId";
                db.Execute(sqlQuery, new { gameId });
            }
        }
    }
}
