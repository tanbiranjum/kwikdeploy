using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Projects.Commands.ProjectUpdate;

public record ProjectUpdateCommand : IRequest
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}

public class ProjectUpdateCommandHandler : IRequestHandler<ProjectUpdateCommand>
{
    private readonly IApplicationDbContext _context;

    public ProjectUpdateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
    {
        var existingEntity = await _context.Projects.Where(x => x.Id != request.Id && x.Name == request.Name).SingleOrDefaultAsync(cancellationToken);

        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Name), "Another project with the same name already exists.")
            });
        }


        var entity = await _context.Projects
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
