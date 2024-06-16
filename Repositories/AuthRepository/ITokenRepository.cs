using Microsoft.AspNetCore.Identity;

namespace RamsTrackerAPI.Repositories.AuthRepository
{
    public interface ITokenRepository
    {
       string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
