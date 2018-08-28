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



        public DataBaseContext() : base(Config.ConnectionStringForEF)
        { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamePlayer>()
                .HasRequired(s => s.Game)
                .WithMany(g => g.GamePlayers)
                .HasForeignKey(f => f.GameId);

            modelBuilder.Entity<GamePlayer>()
                .HasRequired(s => s.Player)
                .WithMany(g => g.GamePlayers)
                .HasForeignKey(f => f.PlayerId);

            modelBuilder.Entity<PlayerCard>()
                .HasRequired(s => s.GamePlayer)
                .WithMany(g => g.PlayerCards)
                .HasForeignKey(f => f.GamePlayerId);
        }
    }
}
