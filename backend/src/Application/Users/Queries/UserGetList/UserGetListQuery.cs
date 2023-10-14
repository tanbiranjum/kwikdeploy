using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KwikDeploy.Application.Users.Queries.UserGetList;

public class UserGetListQuery : IRequest<PaginatedList<UserHeadDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class UserGetListQueryHandler : IRequestHandler<UserGetListQuery, PaginatedList<UserHeadDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserGetListQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

    }

    public async Task<PaginatedList<UserHeadDto>> Handle(UserGetListQuery request, CancellationToken cancellationToken)
    {
        return await _userManager.Users.OrderBy(x => x.UserName)
            .Select(x => new UserHeadDto
            {
                Id = x.Id, UserName = x.UserName
            }).PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}