using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Commands.TargetDelete;

public record TargetDeleteCommand(int Id) : IRequest;

public class TargetDeleteCommandHandler : IRequestHandler<TargetDeleteCommand>
{
    private readonly IApplicationDbContext _context;

    public TargetDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(TargetDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Targets
                        .Where(x => x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        _context.Targets.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
