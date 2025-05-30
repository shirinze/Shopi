using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopi.Application.Commands.User.Create;
using Shopi.Application.Commands.User.Delete;
using Shopi.Application.Commands.User.ToggleActivation;
using Shopi.Application.Commands.User.Update;
using Shopi.Application.Queries.User.GetById;
using Shopi.Application.Queries.User.GetList;
using Shopi.DomainService.BaseSpecifications;
using Shopi.WebApi.DTOs.UserDtos;
using Shopi.WebApi.Features;

namespace Shopi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto input,CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(input.Name, input.LastName, input.Phone);
        await mediator.Send(command);
        return Ok(BaseResult.Success());
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto input,CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(id,input.Name, input.LastName, input.Phone);
        await mediator.Send(command);
        return Ok(BaseResult.Success());
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id,CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        await mediator.Send(command);
        return Ok(BaseResult.Success());
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdUser([FromRoute] int id,CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var entity = await mediator.Send(query,cancellationToken);
        return Ok(BaseResult.Success(entity));

    }
    [HttpGet]
    public async Task<IActionResult> GetUserList(
        [FromQuery] string q,
        [FromQuery] OrderType orderType,
        [FromQuery] int pageSize,
        [FromQuery] int pageNumber,
        CancellationToken cancellationToken)
    {
        var query = new GetUserListQuery(q, orderType, pageSize, pageNumber);
        var entities = await mediator.Send(query,cancellationToken);
        return Ok(BaseResult.Success(entities));
    }
    [HttpPut("{id:int}/ToggleActivation")]
    public async Task<IActionResult> ToggleActivation([FromRoute] int id,CancellationToken cancellationToken)
    {
        var command = new ToggleActivationUserCommand(id);
        await mediator.Send(command);
        return Ok(BaseResult.Success());
    }


}
