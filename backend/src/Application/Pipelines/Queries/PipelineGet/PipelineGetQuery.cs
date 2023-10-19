using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Pipelines.Queries.PipelineGet;

public record PipelineGetQuery : IRequest<PipelineDto>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; init; }
}

public class PipelineGetHandler : IRequestHandler<PipelineGetQuery, PipelineDto>
{
    private readonly IApplicationDbContext _context;

    public PipelineGetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PipelineDto> Handle(PipelineGetQuery request, CancellationToken cancellationToken)
    {
        var pipelineDto = await _context.Pipelines.Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                                    .Select(x => new PipelineDto
                                    {
                                        Id = x.Id,
                                        ProjectId = x.ProjectId,
                                        Name = x.Name,
                                    }).SingleOrDefaultAsync();

        if (pipelineDto is null)
        {
            throw new NotFoundException(nameof(PipelineDto), request.Id);
        }

        return pipelineDto;
    }
}
