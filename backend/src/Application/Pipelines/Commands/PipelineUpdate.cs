using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Pipelines.Commands;

public record PipelineUpdate : IRequest
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; set; }

    [FromBody]
    public PipelineUpdateBody Body { get; init; } = null!;
}

public record PipelineUpdateBody
{
    public string Name { get; init; } = null!;
}


public class PipelineUpdateHandler : IRequestHandler<PipelineUpdate>
{
    private readonly IApplicationDbContext _context;

    public PipelineUpdateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PipelineUpdate request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Pipelines.FindAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Pipeline), request.Id);
        }

        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Pipelines
                                .Where(x => x.ProjectId == request.ProjectId
                                            && x.Id != request.Id
                                            && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another pipeline with the same name already exists.")
            });
        }

        // Update
        entity.ProjectId = request.ProjectId;
        entity.Name = request.Body.Name.Trim();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
