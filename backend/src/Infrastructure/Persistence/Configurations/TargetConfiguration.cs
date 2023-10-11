using KwikDeploy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikDeploy.Infrastructure.Persistence.Configurations;

public class TargetConfiguration : IEntityTypeConfiguration<Target>
{
    public void Configure(EntityTypeBuilder<Target> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(t => t.Key)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.ConnectionId)
            .HasMaxLength(100);
    }
}
