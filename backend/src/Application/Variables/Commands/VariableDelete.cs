using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Variables.Commands;

public record VariableDelete : IRequest
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class VariableDeleteHandler : IRequestHandler<VariableDelete>
{
    private readonly IApplicationDbContext _context;

    public VariableDeleteHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(VariableDelete request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Variables
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(VariableValue), request.Id);
        }

        // Delete
        _context.Variables.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
