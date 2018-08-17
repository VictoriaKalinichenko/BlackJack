using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BlackJack.Entity.Models;
using BlackJack.Configuration;
using BlackJack.DAL.Initializer;

namespace BlackJack.DAL.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GamePlayer> GamePlayers { get; set; }

        public DbSet<PlayerCard> PlayerCards { get; set; }



        public DataBaseContext() : base(Config.ConnectionString)
        {
            Database.SetInitializer(new DbInitializer());
        }
    }
}
