using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Blazored.Application.Users.Commands;
using CleanArchitecture.Blazored.Application.Users.Queries;
using CleanArchitecture.Blazored.WebUi.Shared.AccessControl;
using CleanArchitecture.Blazored.WebUi.Shared.Authorization;

namespace CleanArchitecture.Blazored.WebUi.Controllers;

[Route("api/Admin/[controller]")]
public class UsersController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Permissions.ViewUsers | Permissions.ManageUsers)]
    public async Task<ActionResult<UsersVm>> GetUsers()
    {
        return await Mediator.Send(new GetUsersQuery());
    }
    
    [HttpGet("{id}")]
    [Authorize(Permissions.ViewUsers)]
    public async Task<ActionResult<UserDetailsVm>> GetUser(string id)
    {
        return await Mediator.Send(new GetUserQuery(id));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Permissions.ManageUsers)]
    public async Task<IActionResult> PutUser(string id, UserDto updatedUser)
    {
        if (id != updatedUser.Id) return BadRequest();

        await Mediator.Send(new UpdateUserCommand(updatedUser));

        return NoContent();
    }
}
