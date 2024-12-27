using DVWalks_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DVWalks_API.Data
{
    public class DVWalksDbContext : DbContext
    {
        public DVWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
