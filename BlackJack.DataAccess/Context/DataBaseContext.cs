using System.Data.Entity;
using BlackJack.Entities.Models;

namespace BlackJack.DataAccess.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GamePlayer> GamePlayers { get; set; }

        public DbSet<PlayerCard> PlayerCards { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Card> Cards { get; set; }



        public DataBaseContext() : base(BlackJack.Configuration.Config.ConnectionStringForEF)
        { }
    }
}
