using FluentValidation;

namespace KwikDeploy.Application.Projects.Queries.ProjectGetList;

public class ProjectGetListQueryValidator : AbstractValidator<ProjectGetListQuery>
{
    public ProjectGetListQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber should be at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should be at least greater than or equal to 1.");
    }
}
