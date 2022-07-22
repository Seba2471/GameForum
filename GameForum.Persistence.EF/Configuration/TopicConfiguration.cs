using GameForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameForum.Persistence.EF.Configuration
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.AuthorId)
                .IsRequired();

            builder.HasOne(t => t.Author)
                .WithMany(u => u.Topics)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
