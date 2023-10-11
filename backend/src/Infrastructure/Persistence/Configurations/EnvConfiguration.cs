using KwikDeploy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwikDeploy.Infrastructure.Persistence.Configurations;

public class EnvConfiguration : IEntityTypeConfiguration<Env>
{
    public void Configure(EntityTypeBuilder<Env> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(30)
            .IsRequired();
    }
}
