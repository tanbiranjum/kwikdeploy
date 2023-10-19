using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Targets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.AppDefs.Queries;

public record AppDefGet : IRequest<AppDefDto>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; init; }
}

public class AppDefGetHandler : IRequestHandler<AppDefGet, AppDefDto>
{
    private readonly IApplicationDbContext _context;

    public AppDefGetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AppDefDto> Handle(AppDefGet request, CancellationToken cancellationToken)
    {
        var appDefDto = await _context.AppDefs.Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                                    .Select(x => new AppDefDto
                                    {
                                        Id = x.Id,
                                        ProjectId = x.ProjectId,
                                        Name = x.Name,
                                        ImageName = x.ImageName,
                                        Tag = x.Tag,
                                    }).SingleOrDefaultAsync();

        if (appDefDto is null)
        {
            throw new NotFoundException(nameof(TargetDto), request.Id);
        }

        return appDefDto;
    }
}