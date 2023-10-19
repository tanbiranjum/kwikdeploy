using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Queries;

public record TargetGet : IRequest<TargetDto>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; init; }
}

public class TargetGetHandler : IRequestHandler<TargetGet, TargetDto>
{
    private readonly IApplicationDbContext _context;

    public TargetGetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TargetDto> Handle(TargetGet request, CancellationToken cancellationToken)
    {
        var targetDto = await _context.Targets.Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                                    .Select(x => new TargetDto
                                    {
                                        Id = x.Id,
                                        ProjectId = x.ProjectId,
                                        Name = x.Name,
                                    }).SingleOrDefaultAsync();

        if (targetDto is null)
        {
            throw new NotFoundException(nameof(TargetDto), request.Id);
        }

        return targetDto;
    }
}
