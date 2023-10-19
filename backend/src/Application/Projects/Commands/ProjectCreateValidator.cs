using FluentValidation;

namespace KwikDeploy.Application.Projects.Commands;

public class ProjectCreateValidator : AbstractValidator<ProjectCreate>
{
    public ProjectCreateValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
