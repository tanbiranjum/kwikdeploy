using FluentValidation;

namespace KwikDeploy.Application.Users.Queries;

public class UserUniqueNameValidator : AbstractValidator<UserUniqueUserName>
{
    public UserUniqueNameValidator()
    {
        RuleFor(v => v.UserName).NotEmpty();
    }
}