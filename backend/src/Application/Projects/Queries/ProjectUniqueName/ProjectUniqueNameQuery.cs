using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Projects.Queries.ProjectGet;

public record ProjectUniqueNameQuery : IRequest<bool>
{
    public string Name { get; init; } = null!;
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
        var entity = await _context.Projects.Where(x => x.Name == request.Name).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return true;
        }

        return false;
    }
}
