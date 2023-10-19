using FluentValidation;

namespace KwikDeploy.Application.Projects.Commands;

public class ProjectUpdateValidator : AbstractValidator<ProjectUpdate>
{
    public ProjectUpdateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
