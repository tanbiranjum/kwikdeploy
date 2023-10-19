using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Queries;

public record TargetUniqueName : IRequest<Result<bool>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromQuery]
    public string Name { get; init; } = null!;

    [FromQuery]
    public int? TargetId { get; init; } = null;
}

public class TargetUniqueNameHandler : IRequestHandler<TargetUniqueName, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public TargetUniqueNameHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(TargetUniqueName request, CancellationToken cancellationToken)
    {
        if (request.TargetId is null)
        {
            var entity = await _context.Targets
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);
            if (entity is null)
            {
                return new Result<bool>(true);
            }
        }
        else
        {
            var entity = await _context.Targets
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Id != request.TargetId
                                        && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);
            if (entity is null)
            {
                return new Result<bool>(true);
            }
        }

        return new Result<bool>(false);
    }
}