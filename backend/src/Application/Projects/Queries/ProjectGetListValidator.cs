using FluentValidation;

namespace KwikDeploy.Application.Projects.Queries;

public class ProjectGetListValidator : AbstractValidator<ProjectGetList>
{
    public ProjectGetListValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber should be at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should be at least greater than or equal to 1.");
    }
}
