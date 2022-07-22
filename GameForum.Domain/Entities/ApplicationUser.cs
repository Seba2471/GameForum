﻿using Microsoft.AspNetCore.Identity;

namespace GameForum.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
