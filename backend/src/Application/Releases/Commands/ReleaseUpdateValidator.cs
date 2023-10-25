using FluentValidation;

namespace KwikDeploy.Application.Releases.Commands;

public class ReleasUpdateValidator : AbstractValidator<ReleaseUpdate>
{
    public ReleasUpdateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
