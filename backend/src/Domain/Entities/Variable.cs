namespace KwikDeploy.Domain.Entities;

public class Variable : BaseAuditableEntity
{
    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsSecret { get; set; } = true;
}
