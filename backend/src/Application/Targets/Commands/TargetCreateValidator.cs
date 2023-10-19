using FluentValidation;

namespace KwikDeploy.Application.Targets.Commands;

public class TargetCreateValidator : AbstractValidator<TargetCreate>
{
    public TargetCreateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
