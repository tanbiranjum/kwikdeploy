using FluentValidation;

namespace KwikDeploy.Application.Targets.Commands;

public class TargetUpdateValidator : AbstractValidator<TargetUpdate>
{
    public TargetUpdateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
