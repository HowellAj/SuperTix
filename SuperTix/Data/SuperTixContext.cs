using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperTix.Models;

namespace SuperTix.Data
{
    public class SuperTixContext : DbContext
    {
        public SuperTixContext (DbContextOptions<SuperTixContext> options)
            : base(options)
        {
        }

        public DbSet<SuperTix.Models.Category> Category { get; set; } = default!;
        public DbSet<SuperTix.Models.Game> Game { get; set; } = default!;
        public DbSet<SuperTix.Models.Purchase> Purchase { get; set; } = default!;
    }
}
