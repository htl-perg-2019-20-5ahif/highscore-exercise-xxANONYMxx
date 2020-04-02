using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HighscoreLogic
{
    public interface IPlayerContext : IDisposable
    {
        DbSet<Player> Players { get; }
        int SaveChanges();
    }
}
