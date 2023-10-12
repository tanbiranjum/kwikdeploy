namespace KwikDeploy.Domain.Entities;

public class Env : BaseAuditableEntity
{
    public int ProjectId { get; set; }

    public int TargetId { get; set; }

    public string Name { get; set; } = null!;
}
