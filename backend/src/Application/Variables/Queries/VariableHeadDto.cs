using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Variables.Queries;

public class VariableHeadDto : IMapFrom<Variable>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public bool IsSecret { get; init; } = true;
}
