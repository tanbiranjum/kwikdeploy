using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Releases.Commands;

public record ReleaseUpdate : IRequest
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; set; }

    [FromBody]
    public ReleaseUpdateBody Body { get; init; } = null!;
}

public record ReleaseUpdateBody
{
    public string Name { get; init; } = null!;
}


public class ReleaseUpdateHandler : IRequestHandler<ReleaseUpdate>
{
    private readonly IApplicationDbContext _context;

    public ReleaseUpdateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ReleaseUpdate request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Releases.FindAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Release), request.Id);
        }

        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Releases
                                .Where(x => x.ProjectId == request.ProjectId
                                            && x.Id != request.Id
                                            && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another release with the same name already exists.")
            });
        }

        // Update
        entity.ProjectId = request.ProjectId;
        entity.Name = request.Body.Name.Trim();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
