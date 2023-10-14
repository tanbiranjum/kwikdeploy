using FluentValidation;
using KwikDeploy.Application.Users.Queries.UserGet;

namespace Microsoft.Extensions.DependencyInjection.Users.Queries.UserGet;

public class UserGetValidator:AbstractValidator<UserGetQuery>
{
    public UserGetValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.");
    }
}