using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.AppDefs.Commands;

public record AppDefCreate : IRequest<Result<int>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromBody]
    public AppDefCreateBody Body { get; init; } = null!;
}

public record AppDefCreateBody
{
    public string Name { get; init; } = null!;
    public string ImageName { get; init; } = null!;
    public string Tag { get; init; } = null!;
}

public class TargetCreateHandler : IRequestHandler<AppDefCreate, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public TargetCreateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(AppDefCreate request, CancellationToken cancellationToken)
    {
        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.AppDefs
                                    .Where(x => x.ProjectId == request.ProjectId
                                                && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                    .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another app definition with the same name already exists.")
            });
        }

        // Create
        var entity = new AppDef
        {
            ProjectId = request.ProjectId,
            Name = request.Body.Name.Trim(),
            ImageName = request.Body.ImageName,
            Tag = request.Body.Tag,
        };
        _context.AppDefs.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<int>(entity.Id);
    }
}
