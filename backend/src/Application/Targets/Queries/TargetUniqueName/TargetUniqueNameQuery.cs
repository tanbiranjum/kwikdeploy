﻿using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Queries.TargetUniqueName;

public record TargetUniqueNameQuery : IRequest<bool>
{
    public int ProjectId { get; init; }

    public string Name { get; init; } = null!;

    public int? TargetId { get; init; } = null;
}

public class TargetUniqueNameQueryHandler : IRequestHandler<TargetUniqueNameQuery, bool>
{
    private readonly IApplicationDbContext _context;

    public TargetUniqueNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(TargetUniqueNameQuery request, CancellationToken cancellationToken)
    {
        if (request.TargetId == null)
        {
            var entity = await _context.Targets
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
            var entity = await _context.Targets
                            .Where(x => x.ProjectId == request.ProjectId 
                                        && x.Id != request.TargetId 
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