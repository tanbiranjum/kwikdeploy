using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Application.Users.Commands;

public class UserDelete : IRequest
{
    [FromRoute]
    public string Id { get; set; } = default!;
}

public class UserDeleteHandler : IRequestHandler<UserDelete>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserDeleteHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

    }

    public async Task Handle(UserDelete request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user is null)
        {
            throw new ArgumentException("User not found", nameof(request.Id));
        }
        await _userManager.DeleteAsync(user);
    }
}