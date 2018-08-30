using System.Data.Entity;
using BlackJack.Entities.Models;
using BlackJack.Configuration;

namespace BlackJack.DataAccess.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GamePlayer> GamePlayers { get; set; }

        public DbSet<PlayerCard> PlayerCards { get; set; }

        public DbSet<Log> Logs { get; set; }



        public DataBaseContext() : base(Config.ConnectionStringForEF)
        { }
    }
}
