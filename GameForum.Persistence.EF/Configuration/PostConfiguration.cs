using GameForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameForum.Infrastructure.Persistence.EF.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.PostId)
                .IsRequired();

            builder.Property(p => p.AuthorId)
                .IsRequired();

            builder.HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
