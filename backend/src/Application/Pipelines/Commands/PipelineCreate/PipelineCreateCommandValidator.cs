using FluentValidation;

namespace KwikDeploy.Application.Pipelines.Commands.PipelineCreate;

public class PipelineCreateCommandValidator : AbstractValidator<PipelineCreateCommand>
{
    public PipelineCreateCommandValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}

