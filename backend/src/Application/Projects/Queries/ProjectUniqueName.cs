﻿using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Projects.Queries;

public record ProjectUniqueName : IRequest<Result<bool>>
{
    [FromQuery]
    public string Name { get; init; } = null!;

    [FromQuery]
    public int? ProjectId { get; init; } = null;
}

public class ProjectUniqueNameHandler : IRequestHandler<ProjectUniqueName, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public ProjectUniqueNameHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(ProjectUniqueName request, CancellationToken cancellationToken)
    {
        if (request.ProjectId is null || request.ProjectId == 0)
        {
            var entity = await _context.Projects
                            .Where(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                return new Result<bool>(true);
            }
        }
        else
        {
            var entity = await _context.Projects
                            .Where(x => x.Id != request.ProjectId && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                return new Result<bool>(true);
            }
        }

        return new Result<bool>(false);
    }
}