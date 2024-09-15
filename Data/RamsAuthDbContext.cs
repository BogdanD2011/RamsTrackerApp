using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RamsTrackerAPI.Data.Account;

namespace RamsTrackerAPI.Data
{
    public class RamsAuthDbContext : IdentityDbContext<User>
    {
        public RamsAuthDbContext(DbContextOptions<RamsAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var Level_I_RoleId = "4F60EAE4-0AE1-4C9E-BA31-91C5F5232CC7";
            var Level_II_RoleId = "B99383C7-F325-467F-9CC4-4E28BBBFF890";
            var Level_III_RoleId = "70A9E9A7-A94F-4B65-A942-BF946E11D699";
            var Level_IV_RoleId = "6AD221F1-7524-4624-9513-208FA12F28DC";
            var Level_V_RoleId = "3DF7535C-E623-4856-8892-81FA949F591F";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = Level_I_RoleId,
                    ConcurrencyStamp = Level_I_RoleId,
                    Name = "Subcontractor",
                    NormalizedName = "Subcontractor".ToUpper()
                },
                 new IdentityRole
                {
                    Id = Level_II_RoleId,
                    ConcurrencyStamp = Level_II_RoleId,
                    Name = "Contractor",
                    NormalizedName = "Contractor".ToUpper()
                },
                 new IdentityRole
                 {
                     Id = Level_III_RoleId,
                     ConcurrencyStamp = Level_III_RoleId,
                     Name = "H&S_Representant",
                     NormalizedName = "H&S_Representant".ToUpper(),
                 },
                 new IdentityRole
                 {
                     Id = Level_IV_RoleId,
                     ConcurrencyStamp = Level_IV_RoleId,
                     Name = "GroupAdmin",
                     NormalizedName = "GroupAdmin".ToUpper()
                 },
                 new IdentityRole
                 {
                     Id = Level_V_RoleId,
                     ConcurrencyStamp = Level_V_RoleId,
                     Name = "GeneralAdmin",
                     NormalizedName = "GeneralAdmin".ToUpper()
                 }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
