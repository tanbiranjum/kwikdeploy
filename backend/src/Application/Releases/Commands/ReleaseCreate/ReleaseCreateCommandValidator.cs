using FluentValidation;

namespace KwikDeploy.Application.Releases.Commands.ReleaseCreate;

public class ReleaseCreateCommandValidator : AbstractValidator<ReleaseCreateCommand>
{
    public ReleaseCreateCommandValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
