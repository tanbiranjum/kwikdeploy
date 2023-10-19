using FluentValidation;

namespace KwikDeploy.Application.Users.Commands;

public class UserSetEmailValidator : AbstractValidator<UserSetEmail>
{
    public UserSetEmailValidator()
    {
        RuleFor(v => v.Body.Email).NotEmpty().EmailAddress();
        RuleFor(v => v.Id).NotEmpty();
    }
}