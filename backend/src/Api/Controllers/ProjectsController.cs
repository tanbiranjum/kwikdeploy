using System.Threading;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Projects.Commands;
using KwikDeploy.Application.Projects.Queries;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

[Route("Projects")]
public class ProjectsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProjectHeadDto>>> GetList(ProjectGetList query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ProjectDto>> GetById(ProjectGet query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("uniquename")]
    public async Task<ActionResult<Result<bool>>> IsUniqueName(ProjectUniqueName query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<Result<int>>> Create(ProjectCreate command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(ProjectUpdate command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(ProjectDelete command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
