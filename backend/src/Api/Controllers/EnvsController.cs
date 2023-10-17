using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Envs.Commands.EnvCreate;
using KwikDeploy.Application.Envs.Commands.EnvDelete;
using KwikDeploy.Application.Envs.Commands.EnvUpdate;
using KwikDeploy.Application.Envs.Queries.EnvGet;
using KwikDeploy.Application.Envs.Queries.EnvGetList;
using KwikDeploy.Application.Envs.Queries.EnvUniqueName;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

[Route("Envs/{projectId}")]
public class EnvsController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedList<EnvHeadDto>>> GetList(int projectId, [FromQuery] EnvGetList query)
    {
        query.ProjectId = projectId;

        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnvDto>> GetById(int projectId, int id)
    {
        return await Mediator.Send(new EnvGetQuery { ProjectId = projectId, Id = id });
    }

    [HttpGet("uniquename/{name}")]
    public async Task<ActionResult<bool>> IsUniqueName(int projectId, string name)
    {
        return await Mediator.Send(new EnvUniqueNameQuery { ProjectId = projectId, Name = name, EnvId = null });
    }

    [HttpGet("uniquename/{name}/{envId}")]
    public async Task<ActionResult<bool>> IsUniqueNameExludingItself(int projectId, string name, int envId)
    {
        return await Mediator.Send(new EnvUniqueNameQuery { ProjectId = projectId, Name = name, EnvId = envId });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(int projectId, EnvCreateCommand command)
    {
        command.ProjectId = projectId;

        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int projectId, int id, EnvUpdateCommand command)
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
        await Mediator.Send(new EnvDeleteCommand(projectId, id));

        return NoContent();
    }
}