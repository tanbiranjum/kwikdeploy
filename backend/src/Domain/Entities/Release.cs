namespace KwikDeploy.Domain.Entities;

public class Release : BaseAuditableEntity
{
    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;
}
