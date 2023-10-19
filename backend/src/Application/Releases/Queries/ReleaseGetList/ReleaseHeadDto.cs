using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Releases.Queries.ReleaseGetList;
public class ReleaseHeadDto : IMapFrom<Release>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}

