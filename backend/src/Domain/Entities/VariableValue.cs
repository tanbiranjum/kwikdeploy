namespace KwikDeploy.Domain.Entities;

public class VariableValue : BaseAuditableEntity
{
    public int VariableId { get; set; }

    public Variable Variable { get; set; } = null!;

    public int EnvId { get; set; }

    public Env Env { get; set; } = null!;

    public string Value { get; set; } = null!;
}
