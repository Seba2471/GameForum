using GameForum.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameForum.Application.Contracts.Persistence
{
    public interface ITokenRepository<T> : IAsyncRepository<RefreshToken> where T : IdentityUser
    {
        string GenerateAccessToken(T user, IList<string> roles);
        RefreshToken GenereateRefreshToken(T user);
        Task<RefreshToken> GetByTokenValue(string refreshToken);
        bool ValidateRefreshToken(string refreshToken);
    }
}
