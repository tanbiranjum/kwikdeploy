using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Pipelines.Queries.PipelineGetList;

public class PipelineHeadDto : IMapFrom<Pipeline>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}
