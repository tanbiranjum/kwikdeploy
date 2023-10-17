using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Envs.Commands.EnvDelete;

public record EnvDeleteCommand(int projectId, int Id) : IRequest;

public class EnvDeleteCommandHandler : IRequestHandler<EnvDeleteCommand>
{
    private readonly IApplicationDbContext _context;

    public EnvDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EnvDeleteCommand request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Envs
                        .Where(x => x.ProjectId == request.projectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Delete
        _context.Envs.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
