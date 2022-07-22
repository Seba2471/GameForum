using Microsoft.AspNetCore.Identity;

namespace GameForum.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
