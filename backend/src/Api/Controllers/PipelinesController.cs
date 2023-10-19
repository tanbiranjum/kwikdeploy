using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Pipelines.Commands;
using KwikDeploy.Application.Pipelines.Queries;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

[Route("Pipelines/{ProjectId}")]
public class PipelinesController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedList<PipelineHeadDto>>> GetList(PipelineGetList query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<PipelineDto>> GetById(PipelineGet query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("uniqueName")]
    public async Task<ActionResult<Result<bool>>> IsUniqueName(PipelineUniqueName query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Result<int>>> Create(PipelineCreate command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(PipelineUpdate command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(PipelineDelete command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
