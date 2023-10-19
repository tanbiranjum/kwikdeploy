using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Pipelines.Commands.PipelineDelete;

public record PipelineDeleteCommand : IRequest
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class PipelineDeleteCommandHandler : IRequestHandler<PipelineDeleteCommand>
{
    private readonly IApplicationDbContext _context;

    public PipelineDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PipelineDeleteCommand request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Pipelines
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Delete
        _context.Pipelines.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
