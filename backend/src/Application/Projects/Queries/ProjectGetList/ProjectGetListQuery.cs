using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using MediatR;

namespace KwikDeploy.Application.Projects.Queries.ProjectGetList;

public record ProjectGetListQuery : IRequest<PaginatedList<ProjectHeadDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class ProjectGetListQueryHandler : IRequestHandler<ProjectGetListQuery, PaginatedList<ProjectHeadDto>>
{
    private readonly IApplicationDbContext _context;

    public ProjectGetListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ProjectHeadDto>> Handle(ProjectGetListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .OrderBy(x => x.Name)
            .Select(x => new ProjectHeadDto
            {
                Id = x.Id,
                Name = x.Name,
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
