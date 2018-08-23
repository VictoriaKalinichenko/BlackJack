using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using BlackJack.Configuration;
using Dapper;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerCardRepository : IPlayerCardRepository 
    {
        public List<PlayerCard> GetByGamePlayerId(int gamePlayerId)
        {
            List<PlayerCard> playerCards = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                string sqlQuery = "SELECT * FROM PlayerCards WHERE GamePlayerId = @gamePlayerId";
                playerCards = db.Query<PlayerCard>(sqlQuery, new { gamePlayerId }).ToList();
            }
            return playerCards;
        }

        public PlayerCard Get(int id)
        {
            PlayerCard playerCard = null;
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                playerCard = db.Query<PlayerCard>("SELECT * FROM PlayerCards WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return playerCard;
        }

        public PlayerCard Create(PlayerCard playerCard)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                string sqlQuery = "INSERT INTO PlayerCards (GamePlayerId, CardId) VALUES(@GamePlayerId, @CardId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int playerCardId = db.Query<int>(sqlQuery, playerCard).FirstOrDefault();
                playerCard.Id = playerCardId;
            }
            return playerCard;
        }

        public void DeleteByGamePlayerId(int gamePlayerId)
        {
            using (IDbConnection db = new SqlConnection(Config.ConnectionStringForDapper))
            {
                var sqlQuery = "DELETE FROM PlayerCards WHERE GamePlayerId = @gamePlayerId";
                db.Execute(sqlQuery, new { gamePlayerId });
            }
        }
    }
}
