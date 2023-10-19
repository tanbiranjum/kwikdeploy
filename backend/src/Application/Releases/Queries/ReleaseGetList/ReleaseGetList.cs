using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Application.Releases.Queries.ReleaseGetList;

public record ReleaseGetList : IRequest<PaginatedList<ReleaseHeadDto>>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromQuery]
    public int PageNumber { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}

public class ReleaseGetListHandler : IRequestHandler<ReleaseGetList, PaginatedList<ReleaseHeadDto>>
{
    private readonly IApplicationDbContext _context;

    public ReleaseGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ReleaseHeadDto>> Handle(ReleaseGetList request, CancellationToken cancellationToken)
    {
        return await _context.Releases
            .Where(x => x.ProjectId == request.ProjectId)
            .OrderBy(x => x.Name)
            .Select(x => new ReleaseHeadDto
            {
                Id = x.Id,
                Name = x.Name,
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
