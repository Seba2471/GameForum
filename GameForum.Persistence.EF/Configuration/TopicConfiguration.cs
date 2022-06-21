using GameForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameForum.Persistence.EF.Configuration
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
