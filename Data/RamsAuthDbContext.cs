using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RamsTrackerAPI.Data
{
    public class RamsAuthDbContext : IdentityDbContext
    {
        public RamsAuthDbContext(DbContextOptions<RamsAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "4F60EAE4-0AE1-4C9E-BA31-91C5F5232CC7";
            var writerRoleId = "B99383C7-F325-467F-9CC4-4E28BBBFF890";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                },

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
