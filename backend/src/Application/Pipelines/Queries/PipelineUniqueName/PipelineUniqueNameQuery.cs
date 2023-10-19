using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Pipelines.Queries.PipelineUniqueName;

public record PipelineUniqueNameQuery : IRequest<Result<bool>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromQuery]
    public string Name { get; init; } = null!;

    [FromQuery]
    public int? PipelineId { get; init; } = null;
}

public class PipelineUniqueNameQueryHandler : IRequestHandler<PipelineUniqueNameQuery, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public PipelineUniqueNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(PipelineUniqueNameQuery request, CancellationToken cancellationToken)
    {
        if (request.PipelineId is null)
        {
            var entity = await _context.Pipelines
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
            var entity = await _context.Pipelines
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Id != request.PipelineId
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