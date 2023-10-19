using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Projects.Commands;

public record ProjectUpdate : IRequest
{
    [FromRoute]
    public int Id { get; init; }

    [FromBody]
    public ProjectUpdateBody Body { get; init; } = null!;
}

public record ProjectUpdateBody
{
    public string Name { get; init; } = null!;
}

public class ProjectUpdateHandler : IRequestHandler<ProjectUpdate>
{
    private readonly IApplicationDbContext _context;

    public ProjectUpdateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProjectUpdate request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Projects.FindAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        // Unique Name Check
        var existingEntity = await _context.Projects
                                .Where(x => x.Id != request.Id && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another project with the same name already exists.")
            });
        }

        // Update
        entity.Name = request.Body.Name.Trim();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
