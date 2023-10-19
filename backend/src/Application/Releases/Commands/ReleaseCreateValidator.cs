using FluentValidation;

namespace KwikDeploy.Application.Releases.Commands;

public class ReleaseCreateValidator : AbstractValidator<ReleaseCreate>
{
    public ReleaseCreateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
