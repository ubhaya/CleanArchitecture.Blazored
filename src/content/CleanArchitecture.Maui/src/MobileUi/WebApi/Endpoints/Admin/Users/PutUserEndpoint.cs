using CleanArchitecture.Maui.Application.Users.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.Users;

public sealed class PutUserEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("api/Admin/Users/{id}", PutUser)
            .RequireAuthorization(Permissions.ManageUsers)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Users_PutUser")
            .WithGroupName("Users")
            .WithTags(["Users"]);
    }

    private async Task<IResult> PutUser([FromRoute] string id, [FromBody] UserDto updatedUser, ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != updatedUser.Id) return Results.BadRequest();
        await sender.Send(new UpdateUserCommand(updatedUser), cancellationToken);
        return Results.NoContent();
    }
}