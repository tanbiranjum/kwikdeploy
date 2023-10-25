using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Application.Variables.Queries;

public record VariableGetList : IRequest<PaginatedList<VariableHeadDto>>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromQuery]
    public int PageNumber { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}

public class VariableGetListHandler : IRequestHandler<VariableGetList, PaginatedList<VariableHeadDto>>
{
    private readonly IApplicationDbContext _context;

    public VariableGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<VariableHeadDto>> Handle(VariableGetList request, CancellationToken cancellationToken)
    {
        return await _context.Variables
            .Where(x => x.ProjectId == request.ProjectId)
            .OrderBy(x => x.Name)
            .Select(x => new VariableHeadDto
            {
                Id = x.Id,
                Name = x.Name,
                IsSecret = x.IsSecret,
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
