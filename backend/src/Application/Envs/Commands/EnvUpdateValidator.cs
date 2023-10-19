using FluentValidation;


namespace KwikDeploy.Application.Envs.Commands;
public class EnvUpdateValidator : AbstractValidator<EnvUpdate>
{
    public EnvUpdateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
