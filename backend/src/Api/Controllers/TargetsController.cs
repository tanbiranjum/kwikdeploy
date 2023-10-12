using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Projects.Queries.ProjectGet;
using KwikDeploy.Application.Targets.Commands.TargetCreate;
using KwikDeploy.Application.Targets.Commands.TargetDelete;
using KwikDeploy.Application.Targets.Commands.TargetRegenerateKey;
using KwikDeploy.Application.Targets.Commands.TargetUpdate;
using KwikDeploy.Application.Targets.Queries.TargetGet;
using KwikDeploy.Application.Targets.Queries.TargetGetList;
using KwikDeploy.Application.Targets.Queries.TargetUniqueName;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

public class TargetsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TargetHeadDto>>> GetList([FromQuery] TargetGetList query)
    {
        return await Mediator.Send(query);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<TargetDto>> GetById(int id)
    {
        return await Mediator.Send(new TargetGetQuery { Id = id });
    }

    [HttpGet("uniquename/{name}")]
    public async Task<ActionResult<bool>> IsUniqueName(string name)
    {
        return await Mediator.Send(new TargetUniqueNameQuery { Name = name, TargetId = null });
    }


    [HttpGet("uniquename/{name}/{targetId}")]
    public async Task<ActionResult<bool>> IsUniqueNameExludingItself(string name, int targetId)
    {
        return await Mediator.Send(new TargetUniqueNameQuery { Name = name, TargetId = targetId });
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
