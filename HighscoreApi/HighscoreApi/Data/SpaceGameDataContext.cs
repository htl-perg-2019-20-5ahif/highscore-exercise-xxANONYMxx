using HighscoreLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighscoreApi.Data
{
    public class SpaceGameDataContext : DbContext, IPlayerContext
    {
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(e =>
            {
                e.HasData(
                    new Player { PlayerId = 1, PName = "BRU", Score = 17383 },
                    new Player { PlayerId = 2, PName = "BRO", Score = 1738 },
                    new Player { PlayerId = 3, PName = "BRA", Score = 1783 },
                    new Player { PlayerId = 4, PName = "BRE", Score = 1383 },
                    new Player { PlayerId = 5, PName = "BRI", Score = 383 });
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = Highscore; Trusted_Connection=True; ");
        }
    }
}
