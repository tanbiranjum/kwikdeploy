using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Projects.Queries;

public class ProjectDto : IMapFrom<Project>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}
