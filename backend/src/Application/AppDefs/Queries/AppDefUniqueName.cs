using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.AppDefs.Queries;

public record AppDefUniqueName : IRequest<Result<bool>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromQuery]
    public string Name { get; init; } = null!;

    [FromQuery]
    public int? AppDefId { get; init; } = null;
}

public class AppDefUniqueNameHandler : IRequestHandler<AppDefUniqueName, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public AppDefUniqueNameHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(AppDefUniqueName request, CancellationToken cancellationToken)
    {
        if (request.AppDefId is null)
        {
            var entity = await _context.AppDefs
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
            var entity = await _context.AppDefs
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Id != request.AppDefId
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