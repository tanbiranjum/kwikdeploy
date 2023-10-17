using FluentValidation;

namespace KwikDeploy.Application.Envs.Commands.EnvCreate;
public class EnvCreateCommandValidator : AbstractValidator<EnvCreateCommand>
{
    public EnvCreateCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
