using FluentValidation;

namespace KwikDeploy.Application.Projects.Commands.ProjectUpdate;

public class ProjectUpdateCommandValidator : AbstractValidator<ProjectUpdateCommand>
{
    public ProjectUpdateCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
