using MambaProject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MambaProject.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
            builder.Property(x => x.IMageUrl).IsRequired().HasMaxLength(64);

        }

    }
}
