using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Targets.Queries;

public class TargetHeadDto : IMapFrom<Target>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public bool Connected { get; init; }
}
