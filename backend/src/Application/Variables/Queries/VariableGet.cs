using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Variables.Queries;

public record VariableGet : IRequest<VariableDto>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; init; }
}

public class VariableGetHandler : IRequestHandler<VariableGet, VariableDto>
{
    private readonly IApplicationDbContext _context;

    public VariableGetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<VariableDto> Handle(VariableGet request, CancellationToken cancellationToken)
    {
        var variableDto = await _context.Variables.Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                                    .Select(x => new VariableDto
                                    {
                                        Id = x.Id,
                                        ProjectId = x.ProjectId,
                                        Name = x.Name,
                                        IsSecret = x.IsSecret,
                                    }).SingleOrDefaultAsync();

        if (variableDto is null)
        {
            throw new NotFoundException(nameof(VariableDto), request.Id);
        }

        return variableDto;
    }
}
