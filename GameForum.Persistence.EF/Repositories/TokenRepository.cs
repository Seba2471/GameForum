using GameForum.Application.Contracts.Persistence;
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
        public TokenRepository(JSONWebTokensSettings jsonWebTokensSettings)
        {
            _jsonWebTokensSettings = jsonWebTokensSettings;
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


            return GenerateToken(claims, _jsonWebTokensSettings.Key,
                  _jsonWebTokensSettings.Issuer,
                  _jsonWebTokensSettings.Audience, _jsonWebTokensSettings.AccessTokenDurationTime);
        }

        public string GenerateToken(List<Claim> claims, string secret, string issuer, string audience, double durationInMinutes)
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
