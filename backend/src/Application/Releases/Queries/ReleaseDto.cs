using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Releases.Queries;

public class ReleaseDto : IMapFrom<Release>
{
    public int Id { get; init; }

    public int ProjectId { get; init; }

    public string Name { get; init; } = null!;
}
