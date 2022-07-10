using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameForum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register()
        {
            var user = new IdentityUser { UserName = "Test", Email = "test@gmail.com" };
            var result = await _userManager.CreateAsync(user, "T%ajnehaslo123");

            if (result.Succeeded)
            {
                return new OkResult();
            }
            else
            {
                return new ForbidResult();
            }

        }


        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] TokenRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.userName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.password))
            {
                return new BadRequestResult();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gawgawigawviawnviuawhbvuawvaw"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: "GameForum.Api",
                audience: "ForumUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


            return Ok(jwt);
        }


        public class TokenRequest
        {
            public string userName { get; set; }
            public string password { get; set; }
        }
    }
}
