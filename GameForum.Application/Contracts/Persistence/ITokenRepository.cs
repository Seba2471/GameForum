using GameForum.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameForum.Application.Contracts.Persistence
{
    public interface ITokenRepository<T> where T : IdentityUser
    {
        string GenerateAccessToken(T user, IList<string> roles);
        RefreshToken GenereateRefreshToken(T user);
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        bool ValidateRefreshToken(string refreshToken);
    }
}
