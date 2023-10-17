using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Projects.Queries.ProjectUniqueName;

public record ProjectUniqueNameQuery : IRequest<bool>
{
    public string Name { get; init; } = null!;

    public int? ProjectId { get; init; } = null;
}

public class ProjectUniqueNameQueryHandler : IRequestHandler<ProjectUniqueNameQuery, bool>
{
    private readonly IApplicationDbContext _context;

    public ProjectUniqueNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ProjectUniqueNameQuery request, CancellationToken cancellationToken)
    {
        if (request.ProjectId == null)
        {
            var entity = await _context.Projects
                            .Where(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return true;
            }
        }
        else
        {
            var entity = await _context.Projects
                            .Where(x => x.Id != request.ProjectId && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return true;
            }
        }

        return false;
    }
}