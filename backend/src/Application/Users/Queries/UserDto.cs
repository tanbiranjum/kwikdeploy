using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Identity;

namespace KwikDeploy.Application.Users.Queries;

public class UserDto : IMapFrom<ApplicationUser>
{
    public string Id { get; set; } = default!;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}