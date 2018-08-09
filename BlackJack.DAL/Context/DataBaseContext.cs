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

        public DbSet<Game> Games { get; set; }

        public DbSet<Deck> Decks { get; set; }

        
        public DataBaseContext() : base("name=DataBaseContext") { }
             
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Cards);

            modelBuilder.Entity<Card>()
                .HasMany(p => p.Decks)
                .WithMany(c => c.Cards);

            modelBuilder.Entity<Game>()
                .HasMany(p => p.Players)
                .WithOptional(g => g.Game);

            modelBuilder.Entity<Game>()
                .HasOptional(c => c.Deck);
        }
    }
}
