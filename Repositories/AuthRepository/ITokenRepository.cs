using Microsoft.AspNetCore.Identity;
using RamsTrackerAPI.Data.Account;

namespace RamsTrackerAPI.Repositories.AuthRepository
{
    public interface ITokenRepository
    {
       string CreateJwtToken(User user, List<string> roles);
    }
}
