using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Application.Projects.Commands;

public record ProjectDelete : IRequest
{
    [FromRoute]
    public int Id { get; init; }
}

public class ProjectDeleteHandler : IRequestHandler<ProjectDelete>
{
    private readonly IApplicationDbContext _context;

    public ProjectDeleteHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProjectDelete request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Projects.FindAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Delete
        _context.Projects.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
