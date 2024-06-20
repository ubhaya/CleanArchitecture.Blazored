using CleanArchitecture.Maui.Application.Roles.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.Roles;

public sealed class PutRoleEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("api/Admin/Roles/{id}", PutRole)
            .RequireAuthorization(Permissions.ManageRoles)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Roles_PutRoles")
            .WithGroupName("Roles")
            .WithTags(["Roles"]);
    }

    private static async Task<IResult> PutRole([FromRoute] string id, [FromBody] RoleDto updatedRole, ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != updatedRole.Id) return Results.BadRequest();
        await sender.Send(new UpdateRoleCommand(updatedRole), cancellationToken);
        return Results.NoContent();
    }
}