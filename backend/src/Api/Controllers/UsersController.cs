using KwikDeploy.Api.Controllers;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Users.Commands.UserCreate;
using KwikDeploy.Application.Users.Commands.UserDelete;
using KwikDeploy.Application.Users.Queries.UserGet;
using KwikDeploy.Application.Users.Queries.UserGetList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Users.Queries.UserGet;

namespace Api.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<UserHeadDto>>> GetList([FromQuery] UserGetListQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById([FromRoute] string id)
    {
        return await Mediator.Send(new UserGetQuery
        {
            Id = id
        });
    }

    [HttpPost]
    public async Task<ActionResult<ResultWithId<string>>> Create(UserCreateCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete]
    public async Task<ActionResult<Result>> Delete(UserDeleteCommand command)
    {
        return await Mediator.Send(command);
    }
}