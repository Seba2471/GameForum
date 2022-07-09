using GameForum.Application.Security.Contracts;
using GameForum.Application.Security.Models;
using GameForum.Infrastructure.Security.Models;

namespace GameForum.Infrastructure.Security.Manager
{
    public class SignInManager : ISignInManager<MyUser>
    {
        public Task<SignInResult> PasswordSignInAsync
        (string userName, string password, bool isPersistent,
            bool lockoutOnFailure)
        {
            if (password == "12345")
            {
                return Task.FromResult(SignInResult.Success);
            }
            else
            {
                return Task.FromResult(SignInResult.Fail);
            }

        }
    }
}
