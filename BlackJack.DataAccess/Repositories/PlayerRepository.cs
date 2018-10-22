﻿using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(string connectionString) : base(connectionString)
        { }

        public async Task<Player> SelectByName(string name, PlayerType playerType)
        {
            string sqlQuery = @"SELECT Id, Players.Name, Type FROM Players    
                                WHERE Players.Name = @name AND Players.Type = @playerType";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Player player = await db.QueryFirstOrDefaultAsync<Player>(sqlQuery, new { name = name, playerType = playerType });
                return player;
            }
        }

        public async Task<List<Player>> CreateMany(List<Player> players)
        {
            DapperPlusManager.Entity<Player>().Table("Players").Identity("Id");

            using (DbConnection db = new SqlConnection(_connectionString))
            {
                players = await Task.Run(() => db.BulkInsert(players).CurrentItem);
                return players;
            }
        }
    }
}
