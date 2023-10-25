using FluentValidation;

namespace KwikDeploy.Application.Variables.Commands;

public class VariableUpdateValidator : AbstractValidator<VariableUpdate>
{
    public VariableUpdateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(100)
            .NotEmpty();
    }
}
