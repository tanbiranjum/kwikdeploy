using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Envs.Queries.EnvUniqueName;

public record EnvUniqueNameQuery : IRequest<bool>
{
    public int ProjectId { get; init; }

    public string Name { get; init; } = null!;

    public int? EnvId { get; init; } = null;
}

public class EnvUniqueNameQueryHandler : IRequestHandler<EnvUniqueNameQuery, bool>
{
    private readonly IApplicationDbContext _context;

    public EnvUniqueNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(EnvUniqueNameQuery request, CancellationToken cancellationToken)
    {
        if (request.EnvId == null)
        {
            var entity = await _context.Envs
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                return true;
            }
        }
        else
        {
            var entity = await _context.Envs
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Id != request.EnvId
                                        && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                return true;
            }
        }

        return false;
    }
}