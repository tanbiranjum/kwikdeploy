using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;

namespace KwikDeploy.Application.Targets.Commands.TargetCreate;

public record TargetCreateCommand : IRequest<int>
{
    public int ProjectId { get; init; }

    public string Name { get; init; } = null!;
}

public class TargetCreateCommandHandler : IRequestHandler<TargetCreateCommand, int>
{
    private readonly IApplicationDbContext _context;

    public TargetCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(TargetCreateCommand request, CancellationToken cancellationToken)
    {
        var entity = new Target
        {
            ProjectId = request.ProjectId,
            Name = request.Name,
            Key = Guid.NewGuid().ToString(),
            ConnectionId = null
        };

        _context.Targets.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
