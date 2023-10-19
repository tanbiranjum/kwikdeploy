using FluentValidation;

namespace KwikDeploy.Application.Users.Queries;

public class UserUniqueEmailValidator : AbstractValidator<UserUniqueEmail>
{
    public UserUniqueEmailValidator()
    {
        RuleFor(v => v.Email).NotEmpty().EmailAddress();
    }
}