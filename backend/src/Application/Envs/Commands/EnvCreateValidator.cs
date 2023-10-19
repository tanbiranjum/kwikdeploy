using FluentValidation;

namespace KwikDeploy.Application.Envs.Commands;

public class EnvCreateValidator : AbstractValidator<EnvCreate>
{
    public EnvCreateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
