using FluentValidation;

namespace KwikDeploy.Application.Targets.Commands.TargetCreate;

public class TargetCreateCommandValidator : AbstractValidator<TargetCreateCommand>
{
    public TargetCreateCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
