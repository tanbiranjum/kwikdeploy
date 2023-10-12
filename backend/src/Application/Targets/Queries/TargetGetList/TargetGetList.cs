using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using MediatR;

namespace KwikDeploy.Application.Targets.Queries.TargetGetList;

public record TargetGetList : IRequest<PaginatedList<TargetHeadDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class TargetGetListHandler : IRequestHandler<TargetGetList, PaginatedList<TargetHeadDto>>
{
    private readonly IApplicationDbContext _context;

    public TargetGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TargetHeadDto>> Handle(TargetGetList request, CancellationToken cancellationToken)
    {
        return await _context.Targets
            .OrderBy(x => x.Name)
            .Select(x => new TargetHeadDto
            {
                Id = x.Id,
                Name = x.Name,
                Connected = x.ConnectionId != null,
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
