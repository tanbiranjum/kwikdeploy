using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Targets.Commands.TargetCreate;
using KwikDeploy.Application.Targets.Commands.TargetDelete;
using KwikDeploy.Application.Targets.Commands.TargetRegenerateKey;
using KwikDeploy.Application.Targets.Commands.TargetUpdate;
using KwikDeploy.Application.Targets.Queries.TargetGetList;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

public class TargetsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TargetHeadDto>>> GetTargetsList([FromQuery] TargetGetList query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(TargetCreateCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, TargetUpdateCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("id in URL and body should match.");
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new TargetDeleteCommand(id));

        return NoContent();
    }

    [HttpPost("{id}/regeneratekey")]
    public async Task<ActionResult<string>> RegenerateKey(int id)
    {
        return await Mediator.Send(new TargetRegenerateKeyCommand { Id = id });
    }
}
