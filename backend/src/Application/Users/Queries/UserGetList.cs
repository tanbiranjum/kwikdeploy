using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Application.Users.Queries;

public class UserGetList : IRequest<PaginatedList<UserHeadDto>>
{
    [FromQuery]
    public int PageNumber { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}

public class UserGetListHandler : IRequestHandler<UserGetList, PaginatedList<UserHeadDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserGetListHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

    }

    public async Task<PaginatedList<UserHeadDto>> Handle(UserGetList request, CancellationToken cancellationToken)
    {
        var paginatedList = await _userManager.Users.OrderBy(x => x.UserName)
            .Select(x => new UserHeadDto
            {
                Id = x.Id,
                UserName = x.UserName
            }).PaginatedListAsync(request.PageNumber, request.PageSize);

        return paginatedList;
    }
}