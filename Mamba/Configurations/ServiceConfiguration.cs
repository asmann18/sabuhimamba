using MambaProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MambaProject.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(256);
            builder.Property(x=>x.Icon).IsRequired().HasMaxLength(64);
        }
        
    }
}
