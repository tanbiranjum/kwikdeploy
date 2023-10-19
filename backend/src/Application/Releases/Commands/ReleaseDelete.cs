using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Releases.Commands;

public record ReleaseDelete : IRequest
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class ReleaseDeleteHandler : IRequestHandler<ReleaseDelete>
{
    private readonly IApplicationDbContext _context;

    public ReleaseDeleteHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ReleaseDelete request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Releases
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Delete
        _context.Releases.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
