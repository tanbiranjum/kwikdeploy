using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;

namespace KwikDeploy.Application.Targets.Commands.TargetUpdate;

public record TargetUpdateCommand : IRequest
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}

public class TargetUpdateCommandHandler : IRequestHandler<TargetUpdateCommand>
{
    private readonly IApplicationDbContext _context;

    public TargetUpdateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(TargetUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Targets
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
