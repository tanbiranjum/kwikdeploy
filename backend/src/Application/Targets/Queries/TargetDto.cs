using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Targets.Queries;

public class TargetDto : IMapFrom<Target>
{
    public int Id { get; init; }

    public int ProjectId { get; init; }

    public string Name { get; init; } = null!;
}
