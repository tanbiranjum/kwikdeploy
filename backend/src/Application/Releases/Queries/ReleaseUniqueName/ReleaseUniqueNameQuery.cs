using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Releases.Queries.ReleaseUniqueName;

public record ReleaseUniqueNameQuery : IRequest<Result<bool>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromQuery]
    public string Name { get; init; } = null!;

    [FromQuery]
    public int? ReleaseId { get; init; } = null;
}

public class ReleaseUniqueNameQueryHandler : IRequestHandler<ReleaseUniqueNameQuery, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public ReleaseUniqueNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(ReleaseUniqueNameQuery request, CancellationToken cancellationToken)
    {
        if (request.ReleaseId is null)
        {
            var entity = await _context.Releases
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
            var entity = await _context.Releases
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Id != request.ReleaseId
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