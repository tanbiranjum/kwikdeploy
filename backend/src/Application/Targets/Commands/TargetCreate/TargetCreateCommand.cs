using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Commands.TargetCreate;

public record TargetCreateCommand : IRequest<int>
{
    public int ProjectId { get; init; }

    public string Name { get; init; } = null!;
}

public class TargetCreateCommandHandler : IRequestHandler<TargetCreateCommand, int>
{
    private readonly IApplicationDbContext _context;

    public TargetCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(TargetCreateCommand request, CancellationToken cancellationToken)
    {
        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity == null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Targets.Where(x => x.Name == request.Name).SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Name), "Another target with the same name already exists.")
            });
        }

        // Create
        var entity = new Target
        {
            ProjectId = request.ProjectId,
            Name = request.Name,
            Key = Guid.NewGuid().ToString(),
            ConnectionId = null
        };
        _context.Targets.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
