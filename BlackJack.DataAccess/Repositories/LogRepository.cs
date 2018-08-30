using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using NLog;

namespace BlackJack.DataAccess.Repositories
{
    public class LogRepository : ILogRepository
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string _connectionString;


        public LogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            string sqlQuery = "SELECT Id, DateTime, Message, GameId FROM Logs";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var logs = await db.QueryAsync<Log>(sqlQuery);
                    return logs;
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogGameIsCreated(int gameId, int stage)
        {
            try
            {
                string message = $"Game(Id={gameId}, Stage={stage}) is created";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogPlayerIsAddedToGame(int gameId, Player player, int score)
        {
            try
            {
                string playerType = GetPlayerType(player);
                string message = $"{playerType}(Id={player.Id}, Name={player.Name}, Score={score}) is added to game";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogRoundIsStarted(int gameId)
        {
            try
            {
                string message = "New round is started";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogBetIsCreated(int gameId, Player player, int score, int bet)
        {
            try
            {
                string playerType = GetPlayerType(player);
                string message = $"{playerType}(Id={player.Id}, Name={player.Name}, Score={score}) created the bet(={bet})";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogCardIsAdded(int gameId, Player player, int roundScore, int cardId, int cardValue, string cardName)
        {
            try
            {
                string playerType = GetPlayerType(player);
                string message = $"Card(Id={cardId}, Value={cardValue}, Name={cardName}) is added to {playerType}(Id={player.Id}, Name={player.Name}, RoundScore={roundScore})";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogDealerBjDanger(int gameId, Player player, int cardId, int cardValue, string cardName)
        {
            try
            {
                string playerType = "Dealer";
                string message = $"{playerType}(Id={player.Id}, Name={player.Name}) has BlackJackDanger. His first card is (Id={cardId}, Value={cardValue}, Name={cardName})";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogPlayerBj(int gameId, Player player, int roundScore, float betPayCoef)
        {
            try
            {
                string playerType = GetPlayerType(player);
                string message = $"{playerType}(Id={player.Id}, Name={player.Name}) has Blackjack(RoundScore={roundScore}). BetPayCoefficient is changed(={betPayCoef})";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogPlayerBjAndDealerBjDanger(int gameId, Player player, int roundScore, float betPayCoef)
        {
            try
            {
                string playerType = GetPlayerType(player);
                string message = $"{playerType}(Id={player.Id}, Name={player.Name}) has Blackjack(RoundScore={roundScore}) with DealerBlackJackDanger. BetPayCoefficient is changed(={betPayCoef})";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        public async Task CreateLogGameStageIsChanged(int gameId, int stage)
        {
            try
            {
                string message = $"Stage is changed (Stage={stage})";
                Log log = new Log() { DateTime = DateTime.Now, GameId = gameId, Message = message };
                await Create(log);
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private string GetPlayerType(Player player)
        {
            try
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
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }

        private async Task Create(Log log)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    await db.InsertAsync(log);
                }
            }
            catch (Exception ex)
            {
                string message = $"{ex.Source}|{ex.TargetSite}|{ex.StackTrace}|{ex.Message}";
                _logger.Error(message);
                throw ex;
            }
        }
    }
}
