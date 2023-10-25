using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Variables.Commands;

public record VariableUpdate : IRequest
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; set; }

    [FromBody]
    public VariableUpdateBody Body { get; init; } = null!;
}

public record VariableUpdateBody
{
    public string Name { get; init; } = null!;
    public bool IsSecret { get; init; } = true;
}


public class VariableUpdateHandler : IRequestHandler<VariableUpdate>
{
    private readonly IApplicationDbContext _context;

    public VariableUpdateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(VariableUpdate request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Variables.FindAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Variable), request.Id);
        }

        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Variables
                                .Where(x => x.ProjectId == request.ProjectId
                                            && x.Id != request.Id
                                            && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another variable with the same name already exists.")
            });
        }

        // Update
        entity.ProjectId = request.ProjectId;
        entity.Name = request.Body.Name.Trim();
        entity.IsSecret = request.Body.IsSecret;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
