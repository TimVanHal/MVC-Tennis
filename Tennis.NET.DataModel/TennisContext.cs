using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.NET.Models;

namespace Tennis.NET.DataModel
{
    public class TennisContext : DbContext
    {
        public DbSet<Division> Divisions { get; set; }

        public DbSet<Fine> Fines { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }
        
        public DbSet<Role> Roles { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamPlayer> TeamPlayers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasRequired(c => c.Player1)
                .WithMany()
                .HasForeignKey(c => c.Player1Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired(c => c.Player2)
                .WithMany()
                .HasForeignKey(c => c.Player2Id)
                .WillCascadeOnDelete(false);
        }
    }
}
