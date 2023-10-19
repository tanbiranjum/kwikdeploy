using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.AppDefs.Commands;

public record AppDefDelete : IRequest
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class AppDefDeleteHandler : IRequestHandler<AppDefDelete>
{
    private readonly IApplicationDbContext _context;

    public AppDefDeleteHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AppDefDelete request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.AppDefs
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Delete
        _context.AppDefs.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

