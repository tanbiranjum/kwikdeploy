using FluentValidation;
using KwikDeploy.Application.Pipelines.Commands;

namespace KwikDeploy.Application.Variables.Commands;

public class VariableCreateValidator : AbstractValidator<PipelineCreate>
{
    public VariableCreateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(100)
            .NotEmpty();
    }
}

