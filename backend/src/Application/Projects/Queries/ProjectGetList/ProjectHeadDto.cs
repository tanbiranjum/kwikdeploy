using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Projects.Queries.ProjectGetList;

public class ProjectHeadDto : IMapFrom<Project>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}
