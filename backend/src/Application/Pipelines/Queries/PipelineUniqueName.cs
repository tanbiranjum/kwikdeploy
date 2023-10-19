using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Pipelines.Queries;

public record PipelineUniqueName : IRequest<Result<bool>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromQuery]
    public string Name { get; init; } = null!;

    [FromQuery]
    public int? PipelineId { get; init; } = null;
}

public class PipelineUniqueNameHandler : IRequestHandler<PipelineUniqueName, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public PipelineUniqueNameHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(PipelineUniqueName request, CancellationToken cancellationToken)
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