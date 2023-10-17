using FluentValidation;


namespace KwikDeploy.Application.Envs.Commands.EnvUpdate;
public class EnvUpdateCommandValidator : AbstractValidator<EnvUpdateCommand>
{
    public EnvUpdateCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
