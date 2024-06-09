using Microsoft.EntityFrameworkCore;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Data
{
    public class RamsDbContext : DbContext
    {
        public RamsDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<RA> RA { get; set; }

        public DbSet<Subcontractor> Subcontractor { get; set; }
        public DbSet<MS> MS { get; set; }
    }
}
