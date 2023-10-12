using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Projects.Queries.ProjectGet;

public record ProjectGetQuery : IRequest<ProjectDto>
{
    public int Id { get; init; }
}

public class ProjectGetHandler : IRequestHandler<ProjectGetQuery, ProjectDto>
{
    private readonly IApplicationDbContext _context;

    public ProjectGetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto> Handle(ProjectGetQuery request, CancellationToken cancellationToken)
    {
        var projectDto = await _context.Projects.Where(x=>x.Id == request.Id)
                                    .Select(x => new ProjectDto
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                    }).SingleOrDefaultAsync();

        if (projectDto == null)
        {
            throw new NotFoundException(nameof(ProjectDto), request.Id);
        }

        return projectDto;
    }
}
