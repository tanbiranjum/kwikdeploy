using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Users.Commands;

public record UserSetUserName : IRequest
{
    [FromRoute]
    public string Id { get; init; } = default!;

    [FromBody]
    public UserSetUserNameBody Body { get; init; } = null!;
}

public record UserSetUserNameBody
{
    public string UserName { get; init; } = default!;
}

public class UserSetUserNameHandler : IRequestHandler<UserSetUserName>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserSetUserNameHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(UserSetUserName request, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(request.Id);
        if (user is null)
        {
            throw new NotFoundException(nameof(UserSetEmail.Id), request.Id);
        }

        var normalizedUserName = _userManager.NormalizeName(request.Body.UserName);
        if (user.NormalizedUserName!.Equals(normalizedUserName))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new(nameof(request.Body.UserName), "The new user name must be different from the current one.")
            });
        }

        if (await _userManager.Users.AnyAsync(x => x.NormalizedUserName == normalizedUserName && x.Id != request.Id,
                cancellationToken))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new(nameof(request.Body.UserName), "Another user with the same user name already exists.")
            });
        }

        var result = await _userManager.SetUserNameAsync(user, request.Body.UserName);
        if (!result.Succeeded)
        {
            var validationErrors = result.Errors.Select(x => new ValidationFailure("", $"{x.Code}: ${x.Description}"));
            throw new ValidationException(new List<ValidationFailure>(validationErrors));
        }
    }
}