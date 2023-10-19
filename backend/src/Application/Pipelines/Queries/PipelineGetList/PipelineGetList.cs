using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Application.Pipelines.Queries.PipelineGetList;

public record PipelineGetList : IRequest<PaginatedList<PipelineHeadDto>>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromQuery]
    public int PageNumber { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}

public class PipelineGetListHandler : IRequestHandler<PipelineGetList, PaginatedList<PipelineHeadDto>>
{
    private readonly IApplicationDbContext _context;

    public PipelineGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<PipelineHeadDto>> Handle(PipelineGetList request, CancellationToken cancellationToken)
    {
        return await _context.Pipelines
            .Where(x => x.ProjectId == request.ProjectId)
            .OrderBy(x => x.Name)
            .Select(x => new PipelineHeadDto
            {
                Id = x.Id,
                Name = x.Name,
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
