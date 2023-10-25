using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Variables.Commands;

public record VariableCreate : IRequest<Result<int>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromBody]
    public VariableCreateBody Body { get; init; } = null!;
}

public record VariableCreateBody
{
    public string Name { get; init; } = null!;
    public bool IsSecret { get; init; } = true;
}

public class VariableCreateHandler : IRequestHandler<VariableCreate, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public VariableCreateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(VariableCreate request, CancellationToken cancellationToken)
    {
        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Variables
                                    .Where(x => x.ProjectId == request.ProjectId
                                                && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                    .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another variable with the same name already exists.")
            });
        }

        // Create
        var entity = new Variable
        {
            ProjectId = request.ProjectId,
            Name = request.Body.Name.Trim(),
            IsSecret = request.Body.IsSecret,
        };
        _context.Variables.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<int>(entity.Id);
    }
}
