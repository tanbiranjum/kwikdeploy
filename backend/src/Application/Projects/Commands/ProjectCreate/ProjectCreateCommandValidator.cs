using FluentValidation;

namespace KwikDeploy.Application.Projects.Commands.ProjectCreate;

public class ProjectCreateCommandValidator : AbstractValidator<ProjectCreateCommand>
{
    public ProjectCreateCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
