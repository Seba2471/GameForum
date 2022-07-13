using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GameForum.Application.Contracts.Persistence
{
    public interface ITokenRepository<T> where T : IdentityUser
    {
        string GenerateAccessToken(T user, IList<string> roles);

        string GenerateToken(List<Claim> claims, string secret, string issuer, string audience,
            double durationTime);
    }
}
