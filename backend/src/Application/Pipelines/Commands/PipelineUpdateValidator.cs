using FluentValidation;

namespace KwikDeploy.Application.Pipelines.Commands;

public class PipelineUpdateValidator : AbstractValidator<PipelineUpdate>
{
    public PipelineUpdateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
