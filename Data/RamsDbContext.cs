using Microsoft.EntityFrameworkCore;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Data
{
    public class RamsDbContext : DbContext
    {
        public RamsDbContext(DbContextOptions<RamsDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<RA> RA { get; set; }

        public DbSet<Subcontractor> Subcontractor { get; set; }
        public DbSet<MS> MS { get; set; }

        public DbSet<Files> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Subcontractor
        }
    }
}
