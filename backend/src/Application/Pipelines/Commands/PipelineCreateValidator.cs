using FluentValidation;

namespace KwikDeploy.Application.Pipelines.Commands;

public class PipelineCreateValidator : AbstractValidator<PipelineCreate>
{
    public PipelineCreateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}

