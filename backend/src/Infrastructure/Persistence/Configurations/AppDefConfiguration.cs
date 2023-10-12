using KwikDeploy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikDeploy.Infrastructure.Persistence.Configurations;

public class AppDefConfiguration : IEntityTypeConfiguration<AppDef>
{
    public void Configure(EntityTypeBuilder<AppDef> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(t => t.ImageName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Tag)
            .HasMaxLength(100)
            .IsRequired();
    }
}
