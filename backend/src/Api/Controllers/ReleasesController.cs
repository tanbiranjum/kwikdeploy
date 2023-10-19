using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Releases.Commands;
using KwikDeploy.Application.Releases.Queries;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

[Route("Releases/{ProjectId}")]
public class ReleasesController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedList<ReleaseHeadDto>>> GetList(ReleaseGetList query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ReleaseDto>> GetById(ReleaseGet query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("uniqueName")]
    public async Task<ActionResult<Result<bool>>> IsUniqueName(ReleaseUniqueName query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Result<int>>> Create(ReleaseCreate command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(ReleaseUpdate command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(ReleaseDelete command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
