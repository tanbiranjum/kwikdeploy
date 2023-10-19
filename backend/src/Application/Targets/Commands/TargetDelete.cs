using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Commands;

public record TargetDelete : IRequest
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class TargetDeleteHandler : IRequestHandler<TargetDelete>
{
    private readonly IApplicationDbContext _context;

    public TargetDeleteHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(TargetDelete request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Targets
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Delete
        _context.Targets.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
