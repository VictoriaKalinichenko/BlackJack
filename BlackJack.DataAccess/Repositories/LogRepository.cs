using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Models;
using Dapper.Contrib.Extensions;

namespace BlackJack.DataAccess.Repositories
{
    public class LogRepository : ILogRepository
    {
        private string _connectionString;


        public LogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var logs = await db.GetAllAsync<Log>();
                return logs;
            }
        }

        public async Task CreateLogGameIsCreated(int gameId, int stage)
        {
            string message = $"Game(Id={gameId}, Stage={stage}) is created";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        public async Task CreateLogPlayerIsAddedToGame(int gameId, Player player, int score)
        {
            string playerType = GetPlayerType(player);
            string message = $"{playerType}(Id={player.Id}, Name={player.Name}, Score={score}) is added to game";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        public async Task CreateLogRoundIsStarted(int gameId)
        {
            string message = "New round is started";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        public async Task CreateLogBetIsCreated(int gameId, Player player, int score, int bet)
        {
            string playerType = GetPlayerType(player);
            string message = $"{playerType}(Id={player.Id}, Name={player.Name}, Score={score}) created the bet(={bet})";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        public async Task CreateLogCardIsAdded(int gameId, Player player, int roundScore, int cardId, int cardValue, string cardName)
        {
            string playerType = GetPlayerType(player);
            string message = $"Card(Id={cardId}, Value={cardValue}, Name={cardName}) is added to {playerType}(Id={player.Id}, Name={player.Name}, RoundScore={roundScore})";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        public async Task CreateLogDealerBjDanger(int gameId, Player player, int cardId, int cardValue, string cardName)
        {
            string playerType = "Dealer";
            string message = $"{playerType}(Id={player.Id}, Name={player.Name}) has BlackJackDanger. His first card is (Id={cardId}, Value={cardValue}, Name={cardName})";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        public async Task CreateLogPlayerBj(int gameId, Player player, int roundScore, float betPayCoef)
        {
            string playerType = GetPlayerType(player);
            string message = $"{playerType}(Id={player.Id}, Name={player.Name}) has Blackjack(RoundScore={roundScore}). BetPayCoefficient is changed(={betPayCoef})";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        public async Task CreateLogPlayerBjAndDealerBjDanger(int gameId, Player player, int roundScore, float betPayCoef)
        {
            string playerType = GetPlayerType(player);
            string message = $"{playerType}(Id={player.Id}, Name={player.Name}) has Blackjack(RoundScore={roundScore}) with DealerBlackJackDanger. BetPayCoefficient is changed(={betPayCoef})";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        public async Task CreateLogGameStageIsChanged(int gameId, int stage)
        {
            string message = $"Stage is changed (Stage={stage})";
            Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
            await Create(log);
        }

        private string GetPlayerType(Player player)
        {
            string playerType = "Bot";

            if (player.IsDealer)
            {
                playerType = "Dealer";
            }
            if (player.IsHuman)
            {
                playerType = "Human";
            }

            return playerType;
        }

        private async Task Create(Log log)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.InsertAsync(log);
            }
        }
    }
}
