using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Commands.TargetRegenerateKey;

public record TargetRegenerateKeyCommand : IRequest<string>
{
    public int ProjectId { get; init; }
    public int Id { get; init; }
}

public class TargetRegenerateKeyCommandHandler : IRequestHandler<TargetRegenerateKeyCommand, string>
{
    private readonly IApplicationDbContext _context;

    public TargetRegenerateKeyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(TargetRegenerateKeyCommand request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Targets
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Update
        entity.Key = Guid.NewGuid().ToString();
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Key;
    }
}
