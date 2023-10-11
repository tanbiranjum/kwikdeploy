namespace KwikDeploy.Domain.Entities;

public class Pipeline : BaseAuditableEntity
{
    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;
}
