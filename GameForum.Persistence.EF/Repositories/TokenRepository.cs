using GameForum.Application.Contracts.Persistence;
using GameForum.Domain.Entities;
using GameForum.Persistence.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameForum.Infrastructure.Persistence.EF.Repositories
{
    public class TokenRepository<T> : ITokenRepository<T> where T : IdentityUser
    {
        private readonly JSONWebTokensSettings _jsonWebTokensSettings;
        private readonly GameForumContext _dbContext;
        public TokenRepository(JSONWebTokensSettings jsonWebTokensSettings, GameForumContext dbContext)
        {
            _jsonWebTokensSettings = jsonWebTokensSettings;
            _dbContext = dbContext;
        }

        public string GenerateAccessToken(T user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            return GenerateToken(claims, _jsonWebTokensSettings.AccessKey,
                  _jsonWebTokensSettings.Issuer,
                  _jsonWebTokensSettings.Audience, _jsonWebTokensSettings.AccessTokenDurationTimeInMinutes);
        }

        public RefreshToken GenereateRefreshToken(T user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var refreshToken = GenerateToken(claims, _jsonWebTokensSettings.RefreshKey,
                _jsonWebTokensSettings.Issuer,
                _jsonWebTokensSettings.Audience, (_jsonWebTokensSettings.RefreshTokenDurationTimeInDay * 24 * 60));


            return new RefreshToken
            {
                Active = true,
                Expiration = DateTime.UtcNow.AddDays(_jsonWebTokensSettings.RefreshTokenDurationTimeInDay),
                RefreshTokenValue = refreshToken,
                Used = false,
                UserId = user.Id
            };
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _jsonWebTokensSettings.Issuer,
                ValidAudience = _jsonWebTokensSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jsonWebTokensSettings.RefreshKey))
            };

            var claims = tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);


            if (claims != null)
            {
                return true;
            }

            return false;
        }

        private string GenerateToken(List<Claim> claims, string secret, string issuer, string audience, double durationInMinutes)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
