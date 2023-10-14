using FluentValidation;

namespace KwikDeploy.Application.Users.Commands.UserCreate;

public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateCommandValidator()
    {
        RuleFor(v => v.UserName).MaximumLength(255).NotEmpty();
        RuleFor(v => v.Email).MaximumLength(255).EmailAddress();
        RuleFor(v => v.Password).MaximumLength(255).NotEmpty();
        RuleFor(v => v.ConfirmPassword)
            .MaximumLength(255).NotEmpty().Equal(v => v.Password).WithMessage("ConfirmPassword must be equal to Password.");
    }
}