using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Projects.Commands.ProjectCreate;
using KwikDeploy.Application.Projects.Commands.ProjectDelete;
using KwikDeploy.Application.Projects.Commands.ProjectUpdate;
using KwikDeploy.Application.Projects.Queries.ProjectGet;
using KwikDeploy.Application.Projects.Queries.ProjectGetList;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

public class ProjectsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProjectHeadDto>>> GetProjectList([FromQuery] ProjectGetListQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(int id)
    {
        return await Mediator.Send(new ProjectGetQuery { Id = id });
    }

    [HttpGet("uniquename/{name}")]
    public async Task<ActionResult<bool>> UniqueProjectName(string name)
    {
        return await Mediator.Send(new ProjectUniqueNameQuery { Name = name, ProjectId = null });
    }


    [HttpGet("uniquename/{name}/{projectId}")]
    public async Task<ActionResult<bool>> UniqueProjectName(string name, int projectId)
    {
        return await Mediator.Send(new ProjectUniqueNameQuery { Name = name, ProjectId = projectId  });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(ProjectCreateCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, ProjectUpdateCommand command)
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
        await Mediator.Send(new ProjectDeleteCommand(id));

        return NoContent();
    }
}
