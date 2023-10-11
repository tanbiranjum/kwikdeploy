using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;

namespace KwikDeploy.Application.Projects.Commands.ProjectDelete;

public record ProjectDeleteCommand(int Id) : IRequest;

public class ProjectDeleteCommandHandler : IRequestHandler<ProjectDeleteCommand>
{
    private readonly IApplicationDbContext _context;

    public ProjectDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        _context.Projects.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
