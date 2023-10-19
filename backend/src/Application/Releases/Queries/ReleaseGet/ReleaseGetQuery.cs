using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Releases.Queries.ReleaseGet;

public record ReleaseGetQuery : IRequest<ReleaseDto>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; init; }
}

public class ReleaseGetHandler : IRequestHandler<ReleaseGetQuery, ReleaseDto>
{
    private readonly IApplicationDbContext _context;

    public ReleaseGetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReleaseDto> Handle(ReleaseGetQuery request, CancellationToken cancellationToken)
    {
        var releaseDto = await _context.Releases.Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                                    .Select(x => new ReleaseDto
                                    {
                                        Id = x.Id,
                                        ProjectId = x.ProjectId,
                                        Name = x.Name,
                                    }).SingleOrDefaultAsync();

        if (releaseDto is null)
        {
            throw new NotFoundException(nameof(ReleaseDto), request.Id);
        }

        return releaseDto;
    }
}

