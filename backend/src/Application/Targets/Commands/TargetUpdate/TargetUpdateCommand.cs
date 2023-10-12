using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Commands.TargetUpdate;

public record TargetUpdateCommand : IRequest
{
    public int Id { get; init; }

    public int ProjectId { get; init; }

    public string Name { get; init; } = null!;
}

public class TargetUpdateCommandHandler : IRequestHandler<TargetUpdateCommand>
{
    private readonly IApplicationDbContext _context;

    public TargetUpdateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(TargetUpdateCommand request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Targets.FindAsync(request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity == null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Targets.Where(x => x.Id != request.Id && x.Name == request.Name).SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Name), "Another target with the same name already exists.")
            });
        }

        // Update
        entity.ProjectId = request.ProjectId;
        entity.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
