using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BlackJack.Entity;

namespace BlackJack.DAL.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerCard> PlayerCards { get; set; }

        
        public DataBaseContext() : base("name=DataBaseContext") { }
             
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasMany(p => p.PlayerCardList)
                .WithOptional(c => c.Card);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.PlayerCardList)
                .WithOptional(c => c.Player);
        }
    }
}
