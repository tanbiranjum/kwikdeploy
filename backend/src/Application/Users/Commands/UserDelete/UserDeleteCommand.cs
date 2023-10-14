using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KwikDeploy.Application.Users.Commands.UserDelete;

public class UserDeleteCommand:IRequest<Result>
{
    public string Id { get; set; } = default!;
}

public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserDeleteCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

    }
    
    public async Task<Result> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user is null)
        {
            throw new ArgumentException("User not found", nameof(request.Id));
        }
        await _userManager.DeleteAsync(user);
        return Result.Success();
    }
}