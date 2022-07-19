﻿using GameForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameForum.Infrastructure.Persistence.EF.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(r => r.Expiration)
                .IsRequired();

            builder.Property(r => r.RefreshTokenValue)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();
        }
    }
}
