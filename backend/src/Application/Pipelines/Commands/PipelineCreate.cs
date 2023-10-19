using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Pipelines.Commands;

public record PipelineCreate : IRequest<Result<int>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromBody]
    public PipelineCreateBody Body { get; init; } = null!;
}

public record PipelineCreateBody
{
    public string Name { get; init; } = null!;
}

public class PipelineCreateHandler : IRequestHandler<PipelineCreate, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public PipelineCreateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(PipelineCreate request, CancellationToken cancellationToken)
    {
        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Pipelines
                                    .Where(x => x.ProjectId == request.ProjectId
                                                && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                    .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another pipeline with the same name already exists.")
            });
        }

        // Create
        var entity = new Pipeline
        {
            ProjectId = request.ProjectId,
            Name = request.Body.Name.Trim(),
        };
        _context.Pipelines.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<int>(entity.Id);
    }
}
