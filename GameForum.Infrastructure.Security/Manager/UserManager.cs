using GameForum.Application.Security.Contracts;
using GameForum.Application.Security.Models;
using GameForum.Infrastructure.Security.Models;
using System.Security.Claims;

namespace GameForum.Infrastructure.Security.Manager
{
    public class UserManager : IUserManager<MyUser>
    {
        public Task<UserManagerResult> CreateAsync(MyUser user, string password)
        {
            StaticUserList.Users().Add(user);

            return Task.FromResult(UserManagerResult.Success);
        }

        public Task<MyUser> FindByEmailAsync(string email)
        {
            string tolower = email.ToLowerInvariant();
            var user = StaticUserList.Users().FirstOrDefault
                (p => p.Email.ToLowerInvariant() == tolower);

            return Task.FromResult(user);
        }

        public Task<MyUser> FindByNameAsync(string userName)
        {
            string tolower = userName.ToLowerInvariant();
            var user = StaticUserList.Users().FirstOrDefault
                (p => p.UserName.ToLowerInvariant() == tolower);

            return Task.FromResult(user);
        }

        public Task<List<Claim>> GetClaimsAsync(MyUser user)
        {
            Claim c = new Claim("MyCos", "MyValue");
            var lis = new List<Claim>();
            lis.Add(c);
            return Task.FromResult(lis);
        }

        public Task<List<string>> GetRolesAsync(MyUser user)
        {
            string c = "User";
            var lis = new List<string>();
            lis.Add(c);
            return Task.FromResult(lis);
        }
    }
}
