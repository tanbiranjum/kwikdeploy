using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Releases.Commands;

public record ReleaseCreate : IRequest<Result<int>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromBody]
    public ReleaseCreateBody Body { get; init; } = null!;
}

public record ReleaseCreateBody
{
    public string Name { get; init; } = null!;
}

public class ReleaseCreateHandler : IRequestHandler<ReleaseCreate, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public ReleaseCreateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(ReleaseCreate request, CancellationToken cancellationToken)
    {
        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Releases
                                    .Where(x => x.ProjectId == request.ProjectId
                                                && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                    .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another release with the same name already exists.")
            });
        }

        // Create
        var entity = new Release
        {
            ProjectId = request.ProjectId,
            Name = request.Body.Name.Trim(),
        };
        _context.Releases.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<int>(entity.Id);
    }
}
