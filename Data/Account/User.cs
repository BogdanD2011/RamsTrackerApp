using Microsoft.AspNetCore.Identity;

namespace RamsTrackerAPI.Data.Account
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Contractor {  get; set; } = string.Empty;
    }
}
