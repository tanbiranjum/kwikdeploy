using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Users.Queries;

public class UserUniqueUserName : IRequest<Result<bool>>
{
    [FromQuery]
    public string UserName { get; set; } = default!;

    [FromQuery]
    public string? Id { get; set; }
}

public class UserUniqueUserNameHandler : IRequestHandler<UserUniqueUserName, Result<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserUniqueUserNameHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<bool>> Handle(UserUniqueUserName request, CancellationToken cancellationToken)
    {
        string normalizedUserName = _userManager.NormalizeName(request.UserName);

        var result = !await _userManager.Users.AnyAsync(x => x.NormalizedUserName == normalizedUserName && x.Id != request.Id,
            cancellationToken);

        return new Result<bool>(result);
    }
}