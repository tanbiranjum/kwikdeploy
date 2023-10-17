using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Targets.Commands.TargetCreate;
using KwikDeploy.Application.Targets.Commands.TargetDelete;
using KwikDeploy.Application.Targets.Commands.TargetRegenerateKey;
using KwikDeploy.Application.Targets.Commands.TargetUpdate;
using KwikDeploy.Application.Targets.Queries.TargetGet;
using KwikDeploy.Application.Targets.Queries.TargetGetList;
using KwikDeploy.Application.Targets.Queries.TargetUniqueName;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

[Route("Targets/{projectId}")]
public class TargetsController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedList<TargetHeadDto>>> GetList(int projectId, [FromQuery] TargetGetList query)
    {
        query.ProjectId = projectId;

        return await Mediator.Send(query);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<TargetDto>> GetById(int projectId, int id)
    {
        return await Mediator.Send(new TargetGetQuery { ProjectId = projectId, Id = id });
    }

    [HttpGet("uniquename/{name}")]
    public async Task<ActionResult<bool>> IsUniqueName(int projectId, string name)
    {
        return await Mediator.Send(new TargetUniqueNameQuery { ProjectId = projectId, Name = name, TargetId = null });
    }


    [HttpGet("uniquename/{name}/{targetId}")]
    public async Task<ActionResult<bool>> IsUniqueNameExludingItself(int projectId, string name, int targetId)
    {
        return await Mediator.Send(new TargetUniqueNameQuery { ProjectId = projectId, Name = name, TargetId = targetId });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> Create(int projectId, TargetCreateCommand command)
    {
        command.ProjectId = projectId;

        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int projectId, int id, TargetUpdateCommand command)
    {
        command.ProjectId = projectId;
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int projectId, int id)
    {
        await Mediator.Send(new TargetDeleteCommand(projectId, id));

        return NoContent();
    }

    [HttpPost("{id}/regeneratekey")]
    public async Task<ActionResult<string>> RegenerateKey(int projectId, int id)
    {
        return await Mediator.Send(new TargetRegenerateKeyCommand { ProjectId = projectId, Id = id });
    }
}
