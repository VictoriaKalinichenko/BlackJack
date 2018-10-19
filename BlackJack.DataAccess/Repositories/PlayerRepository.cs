﻿using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;
using BlackJack.Entities.Enums;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Z.BulkOperations;

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
            DbConnection db = new SqlConnection(_connectionString);
            db.Open();
            var bulkOperation = new BulkOperation(db);
            bulkOperation.DestinationTableName = "Players";
            bulkOperation.ColumnMappings.Add(new ColumnMapping("Id", true));
            bulkOperation.ColumnMappings.Add(new ColumnMapping("Name", "Name"));
            bulkOperation.ColumnMappings.Add(new ColumnMapping("Type", "Type"));
            bulkOperation.ColumnMappings.Add(new ColumnMapping("CreationDate", "CreationDate"));
            bulkOperation.ColumnMappings.Add("Id", ColumnMappingDirectionType.Output);
            await bulkOperation.BulkInsertAsync(players);
            db.Close();

            return players;
        }
    }
}
