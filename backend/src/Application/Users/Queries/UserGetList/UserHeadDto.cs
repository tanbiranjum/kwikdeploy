using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Identity;

namespace KwikDeploy.Application.Users.Queries.UserGetList;

public class UserHeadDto : IMapFrom<ApplicationUser>
{
    public string Id { get; set; } = default!;
    public string? UserName { get; set; }
}