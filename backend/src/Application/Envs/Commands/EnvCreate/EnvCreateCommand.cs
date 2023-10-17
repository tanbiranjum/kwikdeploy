using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Envs.Commands.EnvCreate;

public record EnvCreateCommand : IRequest<int>
{
    public int ProjectId { get; set; }

    public int TargetId { get; init; }

    public string Name { get; init; } = null!;
}

public class EnvCreateCommandHandler : IRequestHandler<EnvCreateCommand, int>
{
    private readonly IApplicationDbContext _context;

    public EnvCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(EnvCreateCommand request, CancellationToken cancellationToken)
    {
        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity == null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Target Existance Check
        var targetEntity = await _context.Targets.FindAsync(request.TargetId, cancellationToken);
        if (targetEntity == null)
        {
            throw new NotFoundException(nameof(Target), request.TargetId);
        }

        // Unique Name Check
        var existingEntity = await _context.Envs
                                    .Where(x => x.ProjectId == request.ProjectId
                                                && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                                    .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Name), "Another environemnt with the same name already exists.")
            });
        }

        // Create
        var entity = new Env
        {
            ProjectId = request.ProjectId,
            TargetId = request.TargetId,
            Name = request.Name.Trim(),
        };
        _context.Envs.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
