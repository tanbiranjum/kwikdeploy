using FluentValidation;

namespace KwikDeploy.Application.Releases.Queries.ReleaseGetList;

public class ReleaseGetListValidator : AbstractValidator<ReleaseGetList>
{
    public ReleaseGetListValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}