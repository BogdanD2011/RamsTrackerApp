using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Data
{
    public class RamsDbContext : DbContext
    {
        public RamsDbContext(DbContextOptions<RamsDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Contractor> Contractor { get; set; }
        public DbSet<ContractorFlow> ContractorFlow { get; set; }
        public DbSet<HsPersonContact> HsPersonContacts { get; set; }
        public DbSet<MS> MS { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<RA> RA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ContractorFlow>().HasKey(t => new { t.ContractorId, t.ProjectId });
            //modelBuilder.Entity<RA>().HasOne(p => p.MS).WithOne(t => t.RA).OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
