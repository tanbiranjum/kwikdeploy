using KwikDeploy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<AppDef> AppDefs { get; }
    DbSet<Env> Envs { get; }
    DbSet<Pipeline> Pipelines { get; }
    DbSet<Project> Projects { get; }
    DbSet<Release> Releases { get; }
    DbSet<Target> Targets { get; }
    DbSet<Variable> Variables { get; }
    DbSet<VariableValue> VariableValues { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
