using FluentValidation;

namespace KwikDeploy.Application.Users.Queries;

public class UserGetValidator : AbstractValidator<UserGet>
{
    public UserGetValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.");
    }
}